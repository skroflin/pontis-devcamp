using DemoApp.Domain.Interfaces.Repositories.Geolocation;
using DemoApp.Domain.Models.Geolocation;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Persistence.Repositories.Geolocation
{
    public interface PlaceRepository : IPlaceRepository
    {
        Task<List<Place>> GetPlacesPaged (TableMetadata? tableMetadata = null);
        Task<List<Place>> GetPlaces();
        Task<int> GetPlacesCount();
        Task<Place> GetPlace(int id);
        Task InsertPlace (Place place);
        Task UpdatePlace (Place place);
        Task DeletePlace (int id);
    }
}
