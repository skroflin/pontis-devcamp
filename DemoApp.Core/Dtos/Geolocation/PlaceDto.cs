using DemoApp.Domain.Models.Geolocation;

namespace DemoApp.Core.Dtos.Geolocation
{
    public record PlaceDto
    {
        public int PlaceId { get; set; }
        public string? PlaceName { get; set; }

        public static PlaceDto CreateDto(Place place)
        {
            return new PlaceDto 
            { 
                PlaceId = place.Id, 
                PlaceName = place.Name 
            };
        }

        public static Place ToModel(PlaceDto placeDto, bool isUpdate = false) 
        {
            var place = new Place
            {
                Id = placeDto.PlaceId,
                Name = placeDto.PlaceName
            };

            if (isUpdate) 
            {
                place.UserModified = Environment.UserDomainName;
                place.DateCreated = DateTime.Now;
            }
            else
            {
                place.UserModified = Environment.UserDomainName;
                place.DateCreated = DateTime.Now;
            }

            return place;
        }
    }
}
