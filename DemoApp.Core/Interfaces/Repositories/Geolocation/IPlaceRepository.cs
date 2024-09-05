using DemoApp.Core.CoreDbEntities;
using DemoApp.Core.Models.Geolocation;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Domain.Interfaces.Repositories.Geolocation
{
    public interface IPlaceRepository
    {
        Task<List<Place>> GetPlacesPaged(TableMetadata? tableMetadata = null);
        Task<List<Place>> GetPlaces();
        Task<int> GetPlacesCount();
        Task<Country> GetCountry(int id);
        Task insertPlace(Place place);
        Task updatePlace(Place place);
        Task deletePlace(Place place);
    }
}
