namespace DemoApp.Domain.Models.Geolocation
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string UserCreated {  get; set; }
        public DateTime DateCreated { get; set; }
        public string ?UserModified { get; set; }
        public DateTime ?DateModified { get; set; }
    }
}
