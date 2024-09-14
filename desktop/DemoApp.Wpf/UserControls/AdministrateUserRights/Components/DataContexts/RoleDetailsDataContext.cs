using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using DemoApp.Domain.Models.Administration;
using DemoApp.Utilities.TypeManagement;
using DemoApp.WPF.UserControls.AdministrateUserRights.Components.ViewModel;
using Microsoft.Data.SqlClient;

namespace DemoApp.WPF.UserControls.AdministrateUserRights.Components.DataContexts
{
    internal class RoleDetailsDataContext : INotifyPropertyChanged
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

        private Role role;
        public Role Role
        {
            get { return role; }
            set { role = value; OnPropertyChanged("Role"); }
        }

        private ObservableCollection<AuthorizationViewModel> authorizations;
        public ObservableCollection<AuthorizationViewModel> Authorizations
        {
            get { return authorizations; }
            set { authorizations = value; OnPropertyChanged("Authorizations"); }
        }

        #endregion

        public RoleDetailsDataContext()
        {
        }

        public void GetDetails()
        {
            Role = new Role();
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("crud_SelectRoles", con))
                {
                    con.Open();

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        HeaderDetails = $"{reader[1]} details";

                        // Role object
                        Role.Id = int.Parse(reader["Id"].ToString());
                        Role.Name = $"{reader["Name"]}";
                        Role.UserCreated = $"{reader["UserCreated"]}";
                        Role.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
                        Role.UserModified = $"{reader["UserModified"]}";
                        Role.DateModified = TypeManagement.TryParseDateTime(reader["DateModified"].ToString());
                    }
                    reader.Close();
                }
            }

            SetAuthorizations();
        }


        public void InsertDetails()
        {
            if (IsFormValid())
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("crud_InsertRoles", con))
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Name", Role.Name);
                        cmd.Parameters.AddWithValue("@UserCreated", Environment.UserName);
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                        // Return inserted ID so Authorizations can use it
                        Role.Id = int.Parse(cmd.ExecuteScalar().ToString());
                    }
                }
                DeleteRoleAuthorizations();
                InsertRoleAuthorizations();

                IsErrorVisible = false;
            }
            else
            {
                IsErrorVisible = true;
            }
        }

        public void UpdateDetails()
        {
            if (IsFormValid())
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    // Update existing role
                    using (var cmd = new SqlCommand("crud_UpdateRoles", con))
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", Role.Id);
                        cmd.Parameters.AddWithValue("@Name", Role.Name);
                        cmd.Parameters.AddWithValue("@UserCreated", Role.UserCreated);
                        cmd.Parameters.AddWithValue("@DateCreated", Role.DateCreated);
                        cmd.Parameters.AddWithValue("@UserModified", Environment.UserName);
                        cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }
                DeleteRoleAuthorizations();
                InsertRoleAuthorizations();

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
                using (var cmd = new SqlCommand("crud_DeleteRoles", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", Role.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        #region Authorization management

        // Update DataGrid
        public void SetAuthorizations()
        {
            var authorizationsViewModel = GetAuthorizations();
            var authorizationViewModelsUpdated = UpdateAuthorizations(authorizationsViewModel);

            Authorizations = new ObservableCollection<AuthorizationViewModel>(authorizationViewModelsUpdated);
        }

        /// <summary>
        /// Get authorizations and create View Model from them
        /// </summary>
        /// <returns></returns>
        private List<AuthorizationViewModel> GetAuthorizations()
        {
            var authorizationsViewModel = new List<AuthorizationViewModel>();
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand($"crud_SelectAuthorizations", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.Text;

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        authorizationsViewModel.Add(new AuthorizationViewModel
                        {
                            IsChecked = false,
                            Id = int.Parse(reader["Id"].ToString()),
                            Name = reader["Name"].ToString()
                        });
                    }
                    reader.Close();
                }
            }
            return authorizationsViewModel;
        }

        /// <summary>
        /// Update exisiting authorizations view model
        /// </summary>
        /// <param name="authorizationsViewModel"></param>
        /// <returns></returns>
        private List<AuthorizationViewModel> UpdateAuthorizations(List<AuthorizationViewModel> authorizationsViewModel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                // Update authorizations
                using (var cmd = new SqlCommand($"SELECT * FROM dbo.fn_GetRoleAuthorizations({Role.Id})", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.Text;

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var authorizationId = TypeManagement.TryParseInt(reader["AuthorizationId"].ToString());
                        foreach (var authorization in authorizationsViewModel)
                        {
                            if (authorizationId == authorization.Id)
                            {
                                authorization.IsChecked = true;
                            }
                        }
                    }
                    reader.Close();
                }
            }
            return authorizationsViewModel;
        }

        private void DeleteRoleAuthorizations()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                // Delete authorizations
                using (var cmd = new SqlCommand("crud_RoleAuthorizationsDelete", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@RoleId", Role.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertRoleAuthorizations()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                // Add new/updated authorizations
                using (var cmd = new SqlCommand("crud_RoleAuthorizationsInsert", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    foreach (var authorization in Authorizations.Where(x => x.IsChecked))
                    {
                        // Remove old parameters
                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@RoleId", Role.Id);
                        cmd.Parameters.AddWithValue("@AuthorizationId", authorization.Id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        #endregion

        private bool IsFormValid()
        {
            var isFormValid = false;
            if (Role.Name != "")
            {
                isFormValid = true;
            }
            return isFormValid;
        }

        public void ResetDetailsForm()
        {
            Id = 0;
            Role = new Role();
            ControlsEnabled = true;

            SetAuthorizations();
        }
    }

}
