using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DemoApp.Utilities.FileManagement;
using DemoApp.Utilities.SqlServerManagement;
using DemoApp.WPF.UserControls.GenerateModels.Models;

namespace DemoApp.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for GenerateModelsUserControl.xaml
    /// </summary>
    public partial class GenerateModelsUserControl : UserControl
    {
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public string FolderPath { get; set; }
        public GenerateModelsUserControl()
        {
            InitializeComponent();
            cbxServers.ItemsSource = SqlServerManagement.GetServerList();
            DataContext = this;
        }

        #region UI Events
        private void CbxServers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServerName = ((ComboBox)sender).SelectedValue.ToString();
            cbxDatabases.ItemsSource = SqlServerManagement.GetDatabaseList(ServerName);
        }

        private void CbxDatabases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatabaseName = ((ComboBox)sender).SelectedValue.ToString();
            var tfe = new List<TableForExport>();
            foreach (var table in SqlServerManagement.GetTableList(ServerName, DatabaseName))
            {
                tfe.Add(new TableForExport()
                {
                    Table = table,
                    IsCheckedForExport = false
                });
            }
            dataGridTables.ItemsSource = tfe;

            var tfv = new List<ViewForExport>();
            foreach (var view in SqlServerManagement.GetViewlist(ServerName, DatabaseName))
            {
                if (view.Schema == "Area")
                {
                    tfv.Add(new ViewForExport()
                    {
                        View = view,
                        IsCheckedForExport = false
                    });
                }
            }
            dataGridView.ItemsSource = tfv;

        }

        #endregion

        #region Generate POCOs
        /// <summary>
        /// Based on used namespace and class we are creating header for future model class
        /// </summary>
        /// <param name="@namespace">Volatile operator</param>
        /// <param name="@class">Volatile operator</param>
        /// <returns></returns>
        private string CreateCSHeader(string @namespace, string @class)
        {
            return $"using System;\n\n" +
                            $"namespace {@namespace}\n" +
                            $"{{\n" +
                            $"\tpublic class {@class}\n" +
                            $"\t{{\n";
        }

        private string CreatePOCOModel(string columnType, string columnName)
        {
            return $"\t\tpublic {columnType} {columnName} {{ get; set; }} \n";
        }

        private string CreateCSFooter()
        {
            return $"\t}}\n}}";
        }

        private void GenerateModelsForSelectedData()
        {
            try
            {
                var mfe = new List<ModelForExport>();
                foreach (var item in dataGridTables.Items)
                {
                    if (item is TableForExport tfe)
                    {
                        var sb = new StringBuilder();
                        if (tfe.IsCheckedForExport)
                        {
                            sb.Append(CreateCSHeader(tBoxNamespace.Text, tfe.Table.Name));
                            foreach (var column in SqlServerManagement.GetColumnList(tfe.Table, null))
                            {
                                sb.Append(CreatePOCOModel(SqlServerHelpers.GetTypeAlias(column), column.Name));
                            }
                            sb.Append(CreateCSFooter());

                            //add model for generating
                            mfe.Add(new ModelForExport()
                            {
                                Model = sb.ToString(),
                                FileName = $"{tfe.Table.Name}.cs" //add posibility for custom creating and/or removing pluralization
                            });
                        }
                    }
                }
                foreach (var item in dataGridView.Items)
                {
                    if (item is ViewForExport vfe)
                    {
                        var sb = new StringBuilder();
                        if (vfe.IsCheckedForExport)
                        {
                            sb.Append(CreateCSHeader(tBoxNamespace.Text, vfe.View.Name));
                            foreach (var column in SqlServerManagement.GetColumnList(null, vfe.View))
                            {
                                sb.Append(CreatePOCOModel(SqlServerHelpers.GetTypeAlias(column), column.Name));
                            }
                            sb.Append(CreateCSFooter());

                            //add model for generating
                            mfe.Add(new ModelForExport()
                            {
                                Model = sb.ToString(),
                                FileName = $"{vfe.View.Name}.cs" //add posibility for custom creating and/or removing pluralization
                            });
                        }
                    }
                }

                if (mfe.Count > 0)
                {
                    CreateFiles(mfe);
                }
                MessageBox.Show("Models were successfully generated.", "Success", MessageBoxButton.OK);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error {e.Message} occured", "Failure", MessageBoxButton.OK);
            }

        }
        #endregion

        #region File management
        /// <summary>
        /// Create models in folder which we defined
        /// </summary>
        /// <param name="modelsForExport"></param>
        private void CreateFiles(List<ModelForExport> modelsForExport)
        {
            foreach (var mfe in modelsForExport)
            {
                FileManagement.CreateFile(FolderPath, mfe.FileName, mfe.Model);
            }
        }
        #endregion

        #region Validation
        private bool CheckForm()
        {
            var isFormValid = false;

            if (cbxServers.SelectedItem == null)
            {
                errorServers.Visibility = Visibility.Visible;
                isFormValid = false;
            }
            else
            {
                errorServers.Visibility = Visibility.Collapsed;
                isFormValid = true;
            }

            if (cbxDatabases.SelectedItem == null)
            {
                errorDatabases.Visibility = Visibility.Visible;
                isFormValid = false;
            }
            else
            {
                errorServers.Visibility = Visibility.Collapsed;
                isFormValid = true;

            }

            if (tBoxNamespace.Text == "")
            {
                errorNamespace.Visibility = Visibility.Visible;
                isFormValid = false;
            }
            else
            {
                errorNamespace.Visibility = Visibility.Collapsed;
                isFormValid = true;
            }

            if (tBoxFolderPath.Text == "")
            {
                errorFolderPath.Visibility = Visibility.Visible;
                isFormValid = false;
            }
            else
            {
                errorFolderPath.Visibility = Visibility.Collapsed;
                isFormValid = true;
            }
            return isFormValid;
        }
        #endregion

        #region Buttons
        private void BtnFolderPath_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.SelectedPath = Environment.CurrentDirectory;
                var result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    FolderPath = dialog.SelectedPath;
                    tBoxFolderPath.Text = FolderPath;
                }
            }
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckForm())
            {
                GenerateModelsForSelectedData();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            cbxServers.SelectedItem = null;
            cbxDatabases.SelectedItem = null;

            if (dataGridTables.ItemsSource != null)
            {
                foreach (var item in dataGridTables.ItemsSource)
                {
                    ((TableForExport)item).IsCheckedForExport = false;
                }
            }

            if (cbxDatabases.ItemsSource != null)
            {
                foreach (var item in dataGridView.ItemsSource)
                {
                    ((ViewForExport)item).IsCheckedForExport = false;
                }
            }

            tBoxNamespace.Text = "";
            tBoxFolderPath.Text = "";

            errorServers.Visibility = Visibility.Collapsed;
            errorDatabases.Visibility = Visibility.Collapsed;
            errorNamespace.Visibility = Visibility.Collapsed;
            errorFolderPath.Visibility = Visibility.Collapsed;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }
        #endregion
    }
}
