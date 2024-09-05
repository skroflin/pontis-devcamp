using DemoApp.Domain.Interfaces.Repositories.Geolocation;
using DemoApp.Domain.Models.Geolocation;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Persistence.Repositories.Geolocation
{
    public interface RegionRepository : IRegionRepository
    {
        Task<List<Region>> GetRegionsPaged(TableMetadata? tableMetadata = null);
        Task<List<Region>> GetRegions();
        Task<int> GetRegionsCount();
        Task<Place> GetPlace(int id);
        Task InsertPlace (Place place);
        Task DeletePlace(Place place);
        Task UpdatePlace(Place place);
    }
}
