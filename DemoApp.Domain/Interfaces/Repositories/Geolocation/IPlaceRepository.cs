using DemoApp.Domain.CoreDbEntities;
using DemoApp.Domain.Models.Geolocation;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Domain.Interfaces.Repositories.Geolocation
{
    public interface IPlaceRepository
    {
        Task<List<Place>> GetPlacesPaged(TableMetadata? tableMetadata = null);
        Task<List<Place>> GetPlaces();
        Task<int> GetPlacesCount();
        Task<Place> GetPlace(int id);
        Task InsertPlace(Place place);
        Task UpdatePlace(Place place);
        Task DeletePlace(int id);
    }
}
