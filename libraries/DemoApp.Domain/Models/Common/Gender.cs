namespace DemoApp.Domain.Models.Common
{
    public class Gender
    {
        public int Id { get; set; }
        public char GenderShort { get; set; }
        public string Name { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? UserModified {  get; set; }
        public DateTime? DateModified { get; set; }

    }
}
