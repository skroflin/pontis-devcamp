namespace DemoApp.Core.Dtos
{
    public class AuthResponseDto
    {
        public string Username { get; set; }
        public string UserRole { get; set; }
        public List<string> RoleAuthorizations { get; set; }
    }
}