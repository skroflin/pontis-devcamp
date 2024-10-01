using System;
using System.ComponentModel;
using System.Windows.Controls;
using DemoApp.Domain.Models.Administration;
using DemoApp.Utilities.TypeManagement;
using Microsoft.Data.SqlClient;

namespace DemoApp.WPF.UserControls.AdministrateUserRights.Components.DataContexts
{
    internal class AuthorizationDetailsDataContext : INotifyPropertyChanged
    {
        private TextBox txtError;
        private readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdminDB"].ConnectionString;

        #region Interface implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Properties
        public int Id { get; set; }
        public string HeaderDetails { get; set; }

        private bool isErrorVisible;
        public bool IsErrorVisible
        {
            get { return isErrorVisible; }
            set { isErrorVisible = value; OnPropertyChanged("IsErrorVisible"); }
        }

        private bool controlsEnabled;
        public bool ControlsEnabled
        {
            get { return controlsEnabled; }
            set { controlsEnabled = value; OnPropertyChanged("ControlsEnabled"); }
        }

        private Authorization authorization;
        public Authorization Authorization
        {
            get { return authorization; }
            set { authorization = value; OnPropertyChanged("Authorization"); }
        }
        #endregion

        public AuthorizationDetailsDataContext() { }

        public void GetDetails()
        {
            Authorization = new Authorization();
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("crud_SelectAuthorizations", con))
                {
                    con.Open();

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        HeaderDetails = $"{reader[1]} details";

                        // Authorization object
                        Authorization.Id = int.Parse(reader["Id"].ToString());
                        Authorization.Name = $"{reader["Name"]}";
                        Authorization.UserCreated = $"{reader["UserCreated"]}";
                        Authorization.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
                        Authorization.UserModified = $"{reader["UserModified"]}";
                        Authorization.DateModified = TypeManagement.TryParseDateTime(reader["DateModified"].ToString());
                    }
                    reader.Close();
                }
            }
        }

        public void InsertDetails()
        {
            if (IsFormValid())
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("crud_InsertAuthorizations", con))
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Name", Authorization.Name);
                        cmd.Parameters.AddWithValue("@UserCreated", Environment.UserName);
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }
                txtError.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                txtError.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public void UpdateDetails()
        {
            if (IsFormValid())
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("crud_UpdateAuthorizations", con))
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", Authorization.Id);
                        cmd.Parameters.AddWithValue("@Name", Authorization.Name);
                        cmd.Parameters.AddWithValue("@UserCreated", Authorization.UserCreated);
                        cmd.Parameters.AddWithValue("@DateCreated", Authorization.DateCreated);
                        cmd.Parameters.AddWithValue("@UserModified", Environment.UserName);
                        cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }
                IsErrorVisible = false;
            }
            else
            {
                IsErrorVisible = true;
            }
        }

        public void DeleteDetails()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("crud_DeleteAuthorizations", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Authorization.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private bool IsFormValid()
        {
            var isFormValid = false;
            if (Authorization.Name != "")
            {
                isFormValid = true;
            }
            return isFormValid;
        }

        public void ResetDetailsForm()
        {
            Id = 0;
            Authorization = new Authorization();
            ControlsEnabled = true;
        }
    }
}
