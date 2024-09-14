namespace DemoApp.Domain.Models.Administration
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? UserModified { get; set; }
        public DateTime? DateModified { get; set; }
        public ICollection<UserApplication> UserApplications { get; set; }

    }
}
