﻿namespace DemoApp.Domain.Models.Geolocation
{
    public class Place
    {
        public int Id { get; set; }
        public string PlaceNationalCode { get; set; }
        public string Name { get; set; }
        public int DistrictId { get; set; }
        public int RegionId { get; set; }
        public string UserCreated {  get; set; }
        public DateTime DateCreated { get; set; }
        public string ?UserModified { get; set; }
        public DateTime ?DateModified { get; set; }
    }
}
