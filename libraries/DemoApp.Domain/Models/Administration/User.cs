namespace DemoApp.Domain.Models.Administration
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsRegistered { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? UserModified { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
