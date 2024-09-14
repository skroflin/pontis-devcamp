using System.Collections.Generic;
using DemoApp.Domain.Models.Administration;

namespace DemoApp.WPF.UserControls.AdministrateUserRights.Components.ViewModel
{
    public class UserApplicationViewModel
    {
        public bool IsChecked { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int ApplicationId { get; set; }

        public Role SelectedRole { get; set; }
        public List<Role> Roles { get; set; }
    }
}
