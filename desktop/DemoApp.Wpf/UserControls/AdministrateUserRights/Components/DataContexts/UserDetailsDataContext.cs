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
    public class UserDetailsDataContext : INotifyPropertyChanged
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

        private User user;
        public User User
        {
            get { return user; }
            set { user = value; OnPropertyChanged("User"); }
        }

        private ObservableCollection<UserApplicationViewModel> userApplications;
        public ObservableCollection<UserApplicationViewModel> UserApplications
        {
            get { return userApplications; }
            set { userApplications = value; OnPropertyChanged("UserApplications"); }

        }
        #endregion

        public UserDetailsDataContext() { }

        public void GetDetails()
        {
            User = new User();
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("crud_SelectUsers", con))
                {
                    con.Open();

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        HeaderDetails = $"{reader[1]} details";

                        // User object
                        User.Id = int.Parse(reader["Id"].ToString());
                        User.Username = $"{reader["Username"]}";
                        User.Password = $"{reader["Password"]}";
                        User.IsActive = bool.Parse(reader["IsActive"].ToString());
                        User.IsRegistered = bool.Parse(reader["IsRegistered"].ToString());
                        User.UserCreated = $"{reader["UserCreated"]}";
                        User.DateCreated = DateTime.Parse(reader["DateCreated"].ToString());
                        User.UserModified = $"{reader["UserModified"]}";
                        User.DateModified = TypeManagement.TryParseDateTime(reader["DateModified"].ToString());
                    }
                    reader.Close();
                }
            }

            SetUserApplications();
            GetRoles();
        }

        public void InsertDetails()
        {
            if (IsFormValid())
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("crud_InsertUsers", con))
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Username", User.Username);
                        cmd.Parameters.AddWithValue("@Password", User.Password);
                        cmd.Parameters.AddWithValue("@IsActive", User.IsActive);
                        cmd.Parameters.AddWithValue("@IsRegistered", User.IsRegistered);
                        cmd.Parameters.AddWithValue("@UserCreated", Environment.UserName);
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }

                DeleteUserApplications();
                InsertUserApplications();
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
                    using (var cmd = new SqlCommand("crud_UpdateUsers", con))
                    {
                        con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", User.Id);
                        cmd.Parameters.AddWithValue("@Username", User.Username);
                        cmd.Parameters.AddWithValue("@Password", User.Password);
                        cmd.Parameters.AddWithValue("@IsActive", User.IsActive);
                        cmd.Parameters.AddWithValue("@IsRegistered", User.IsRegistered);
                        cmd.Parameters.AddWithValue("@UserCreated", User.UserCreated);
                        cmd.Parameters.AddWithValue("@DateCreated", User.DateCreated);
                        cmd.Parameters.AddWithValue("@UserModified", Environment.UserName);
                        cmd.Parameters.AddWithValue("@DateModified", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }
                DeleteUserApplications();
                InsertUserApplications();
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
                using (var cmd = new SqlCommand("crud_DeleteUsers", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", User.Id);

                    cmd.ExecuteNonQuery();
                }
            }
            DeleteUserApplications();
        }

        #region User Applications
        // Set DataGrid with data from DB
        public void SetUserApplications()
        {
            var userApplicationViewModel = GetUserApplications();
            var userApplicationViewModelUpdated = UpdateUserApplications(userApplicationViewModel);

            UserApplications = new ObservableCollection<UserApplicationViewModel>(userApplicationViewModelUpdated);
        }

        /// <summary>
        /// Get applications and create initial view model from them
        /// </summary>
        /// <returns></returns>
        private List<UserApplicationViewModel> GetUserApplications()
        {
            var userApplicationsViewModel = new List<UserApplicationViewModel>();
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand($"crud_SelectApplications", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.Text;

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userApplicationsViewModel.Add(new UserApplicationViewModel
                        {
                            IsChecked = false,
                            ApplicationId = int.Parse(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            UserId = User.Id,
                            RoleId = 0,
                            Roles = GetRoles(),
                            SelectedRole = new Role()
                        });
                    }
                    reader.Close();
                }
            }
            return userApplicationsViewModel;
        }

        /// <summary>
        /// Update exisiting user applications view model
        /// </summary>
        /// <param name="userApplicationViewModel"></param>
        /// <returns></returns>
        private List<UserApplicationViewModel> UpdateUserApplications(List<UserApplicationViewModel> userApplicationsViewModel)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                // Update authorizations
                using (var cmd = new SqlCommand($"SELECT * FROM fn_GetUserApplications({User.Id})", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.Text;

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var roleId = TypeManagement.TryParseInt(reader["RoleId"].ToString());
                        var applicationId = TypeManagement.TryParseInt(reader["ApplicationId"].ToString());
                        var userId = TypeManagement.TryParseInt(reader["UserId"].ToString());
                        foreach (var userApplication in userApplicationsViewModel)
                        {
                            if (userId == userApplication.UserId && applicationId == userApplication.ApplicationId)
                            {
                                userApplication.IsChecked = true;
                                userApplication.UserId = Id;
                                userApplication.SelectedRole = userApplication.Roles?.FirstOrDefault(x => x.Id == roleId);
                            }
                        }
                    }
                    reader.Close();
                }
            }
            return userApplicationsViewModel;
        }

        private void DeleteUserApplications()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                // Delete authorizations
                using (var cmd = new SqlCommand("crud_UserApplicationsDelete", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", User.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertUserApplications()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                // Add new/updated authorizations
                using (var cmd = new SqlCommand("crud_UserApplicationsInsert", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    foreach (var userApplication in UserApplications.Where(x => x.IsChecked))
                    {
                        if (userApplication != null && userApplication.RoleId != 0)
                        {
                            // Remove old parameters
                            cmd.Parameters.Clear();

                            cmd.Parameters.AddWithValue("@UserId", userApplication.UserId);
                            cmd.Parameters.AddWithValue("@ApplicationId", userApplication.ApplicationId);
                            cmd.Parameters.AddWithValue("@RoleId", userApplication.RoleId);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private List<Role> GetRoles()
        {
            var roles = new List<Role>();
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand("crud_SelectRoles", con))
                {
                    con.Open();

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        roles.Add(new Role()
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            Name = $"{reader["Name"]}",
                        });
                    }
                    reader.Close();
                }
            }
            return roles;
        }
        #endregion

        private bool IsFormValid()
        {
            var isFormValid = false;
            if (User.Username != "")
            {
                isFormValid = true;
            }
            return isFormValid;
        }

        public void ResetDetailsForm()
        {
            Id = 0;
            User = new User();
            ControlsEnabled = true;

            SetUserApplications();
        }
    }
}
