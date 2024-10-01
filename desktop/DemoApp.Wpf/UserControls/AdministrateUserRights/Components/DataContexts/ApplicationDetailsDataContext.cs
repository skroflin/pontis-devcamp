using System;
using System.ComponentModel;
using DemoApp.Domain.Models.Administration;
using DemoApp.Utilities.TypeManagement;
using Microsoft.Data.SqlClient;

namespace DemoApp.WPF.UserControls.AdministrateUserRights.Components.DataContexts
{
    internal class ApplicationDetailsDataContext : INotifyPropertyChanged
    {
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

        private Application application;
        public Application Application
        {
            get { return application; }
            set { application = value; OnPropertyChanged("Application"); }
        }
        #endregion

        public ApplicationDetailsDataContext() { }

        public void GetDetails()
        {
            Application = new Application();
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("crud_SelectApplications", con))
                {
                    con.Open();

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        HeaderDetails = $"{reader[1]} details";

                        // Application object
                        Application.Id = int.Parse(reader["Id"].ToString());
                        Application.Name = $"{reader["Name"]}";
                        Application.UserCreated = $"{reader["UserCreated"]}";
                        Application.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
                        Application.UserModified = $"{reader["UserModified"]}";
                        Application.DateModified = TypeManagement.TryParseDateTime(reader["DateModified"].ToString());
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
                    using (var cmd = new SqlCommand("crud_InsertApplications", con))
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Name", Application.Name);
                        cmd.Parameters.AddWithValue("@UserCreated", Environment.UserName);
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                    IsErrorVisible = false;
                }
            }
            else
            {
                IsErrorVisible = true;
            }
        }

        public void UpdateDetails()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("crud_UpdateApplications", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Application.Id);
                    cmd.Parameters.AddWithValue("@Name", Application.Name);
                    cmd.Parameters.AddWithValue("@UserCreated", Application.UserCreated);
                    cmd.Parameters.AddWithValue("@DateCreated", Application.DateCreated);
                    cmd.Parameters.AddWithValue("@UserModified", Environment.UserName);
                    cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDetails()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("crud_DeleteApplications", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Application.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private bool IsFormValid()
        {
            var isFormValid = false;
            if (Application.Name != "")
            {
                isFormValid = true;
            }
            return isFormValid;
        }

        public void ResetDetailsForm()
        {
            Id = 0;
            Application = new Application();
            ControlsEnabled = true;
        }
    }

}
