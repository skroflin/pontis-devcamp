using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using DemoApp.WPF.UserControls.AdministrateUserRights.UserControls;
using Microsoft.Data.SqlClient;

namespace DemoApp.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for AdministrateUserRightsUserControl.xaml
    /// </summary>
    public partial class AdministrateUserRightsUserControl : UserControl, INotifyPropertyChanged
    {
        private readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdminDB"].ConnectionString;
        private string _selectedCategory;

        #region Interface implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Properties

        private ObservableCollection<string> _categories;
        public ObservableCollection<string> Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged("Categories"); }
        }

        private ObservableCollection<Dictionary<int, string>> _categoryData;
        public ObservableCollection<Dictionary<int, string>> CategoryData
        {
            get { return _categoryData; }
            set { _categoryData = value; OnPropertyChanged("CategoryData"); }
        }

        #endregion

        public AdministrateUserRightsUserControl()
        {
            InitializeComponent();
            DataContext = this;

            SetCategories();
        }


        #region Data
        private void SetCategories()
        {
            var categories = new List<string>();
            var queryString = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN ('Applications', 'Authorizations', 'Roles', 'Users')";

            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(queryString, con))
                {
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            categories.Add(reader[i].ToString());
                        }
                    }
                    reader.Close();
                }
            }

            lstvCategories.ItemsSource = categories;
            if (categories.Count > 0)
            {
                lstvCategories.SelectedItem = categories[0];
            }
        }

        private void SetCategoryData(string tableName)
        {
            var categoryData = new Dictionary<int, string>();
            _selectedCategory = tableName;
            var queryString = $"SELECT * FROM {tableName}";

            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(queryString, con))
                {
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var key = 0;
                        var value = "";
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.GetName(i).ToLower().Contains("id"))
                            {
                                key = int.Parse(reader[i].ToString());
                            }
                            if (reader.GetName(i).ToLower().Contains("name") ||
                                reader.GetName(i).ToLower().Contains("title"))
                            {
                                value = reader[i].ToString();
                            }
                        }
                        categoryData.Add(key, value);
                    }
                    reader.Close();
                }
            }

            // Create new object with CategoryData in it
            CategoryData = new ObservableCollection<Dictionary<int, string>>() { categoryData };

            // If collection has any items select first one
            if (CategoryData.Count > 0)
            {
                lstvCategoryData.SelectedItem = CategoryData[0].First();
            }
        }

        private void SetFormDetails(string strId)
        {
            // Remove any children
            contentHolder.Children.Clear();

            var id = int.Parse(strId);
            UserControl control;
            switch (_selectedCategory)
            {
                case "Applications":
                    control = new ApplicationDetails(id);
                    ((ApplicationDetails)control).CollectionChange += AdministrateUserRightsUserControl_CollectionChanged;

                    contentHolder.Children.Add(control);
                    break;
                case "Authorizations":
                    control = new AuthorizationDetails(id);
                    ((AuthorizationDetails)control).CollectionChange += AdministrateUserRightsUserControl_CollectionChanged;

                    contentHolder.Children.Add(control);
                    break;
                case "Roles":
                    control = new RoleDetails(id);
                    ((RoleDetails)control).CollectionChange += AdministrateUserRightsUserControl_CollectionChanged;

                    contentHolder.Children.Add(control);
                    break;
                case "Users":
                    control = new UserDetails(id);
                    ((UserDetails)control).CollectionChange += AdministrateUserRightsUserControl_CollectionChanged;

                    contentHolder.Children.Add(control);
                    break;
                default:
                    control = new ApplicationDetails(id);
                    ((ApplicationDetails)control).CollectionChange += AdministrateUserRightsUserControl_CollectionChanged;
                    _ = contentHolder.Children.Add(control);
                    break;
            }
        }

        private void AdministrateUserRightsUserControl_CollectionChanged(object sender, EventArgs e)
        {
            SetCategoryData(sender.ToString());
        }
        #endregion

        #region UI Events

        private void LstvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lv = sender as ListView;
            string tableName;
            if (lv != null)
            {
                tableName = lv.SelectedItem.ToString();
                SetCategoryData(tableName);
            }
        }


        private void LstvCategoryData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lv = sender as ListView;
            if (lv != null && lv.SelectedItem != null)
            {
                SetFormDetails(((KeyValuePair<int, string>)lv.SelectedItem).Key.ToString());
            }
        }
        #endregion


    }
}
