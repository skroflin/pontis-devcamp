using System;
using System.Windows.Controls;
using DemoApp.WPF.UserControls.AdministrateUserRights.Components.DataContexts;

namespace DemoApp.WPF.UserControls.AdministrateUserRights.UserControls
{
    /// <summary>
    /// Interaction logic for AuthorizationDetails.xaml
    /// </summary>
    public partial class AuthorizationDetails : UserControl
    {
        private readonly AuthorizationDetailsDataContext _ctx;
        public event EventHandler CollectionChange;

        #region Properties

        public bool CanNewExecute { get; set; }
        public bool CanEditExecute { get; set; }
        public bool CanSaveExecute { get; set; }
        public bool CanDeleteExecute { get; set; }
        #endregion

        public AuthorizationDetails(int id)
        {
            InitializeComponent();
            _ctx = new AuthorizationDetailsDataContext() { Id = id };
            DataContext = _ctx;

            _ctx.ControlsEnabled = false;

            CanNewExecute = true;
            CanEditExecute = CanDeleteExecute = (id != null);
            CanSaveExecute = false;

            _ctx.GetDetails();
        }

        #region Commands

        private void NewCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanNewExecute;
        }
        private void EditCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanEditExecute;
        }
        private void SaveCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanSaveExecute;
        }
        private void DeleteCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CanDeleteExecute;
        }

        private void NewCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            CanNewExecute = CanDeleteExecute = CanEditExecute = false;
            CanSaveExecute = true;

            _ctx.ResetDetailsForm();
        }
        private void EditCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            CanEditExecute = CanDeleteExecute = CanNewExecute = false;
            CanSaveExecute = true;

            _ctx.ControlsEnabled = true;
        }
        private void SaveCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            CanEditExecute = CanDeleteExecute = CanNewExecute = true;
            CanSaveExecute = false;

            if (_ctx.Id == null)
            {
                _ctx.InsertDetails();
            }
            else
            {
                _ctx.UpdateDetails();
            }
            _ctx.ControlsEnabled = false;

            CollectionChanged();
        }
        private void DeleteCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            CanEditExecute = CanSaveExecute = CanDeleteExecute = false;
            CanNewExecute = true;

            _ctx.DeleteDetails();
            _ctx.ResetDetailsForm();

            CollectionChanged();
        }
        #endregion

        #region Events
        private void CollectionChanged()
        {
            CollectionChange?.Invoke("Authorizations", new EventArgs());
        }
        #endregion
    }

}
