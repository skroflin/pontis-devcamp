using Microsoft.EntityFrameworkCore;

namespace DemoApp.Domain.Models.Administration
{
    [Keyless]
    public class RoleAuthorization
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int AuthorizationId { get; set; }
        public Authorization Authorization { get; set; }
    }
}
