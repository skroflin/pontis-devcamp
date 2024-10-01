using DemoApp.Domain.Models.Geolocation;

namespace DemoApp.Core.Dtos.Geolocation
{
    public record PlaceDto
    {
        public int? PlaceId { get; set; }
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
                Name = placeDto.PlaceName
            };

            if (isUpdate && placeDto.PlaceId.HasValue)
            {
                place.Id = placeDto.PlaceId.Value;
                place.UserModified = Environment.UserDomainName;
                place.DateModified = DateTime.Now;
            }
            else
            {
                place.UserCreated = Environment.UserDomainName;
                place.DateCreated = DateTime.Now;
            }

            return place;
        }
    }
}
