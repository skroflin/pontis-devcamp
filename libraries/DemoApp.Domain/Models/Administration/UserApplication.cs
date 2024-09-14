using System.Text.Json.Serialization;

namespace DemoApp.Domain.Models.Administration
{
    public class UserApplication
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int ApplicationId { get; set; }
        [JsonIgnore]
        public Application Application { get; set; }
        public int RoleId { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }
    }
}
