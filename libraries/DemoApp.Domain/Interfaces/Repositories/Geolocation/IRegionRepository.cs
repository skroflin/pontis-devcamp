using DemoApp.Domain.Models.Geolocation;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Domain.Interfaces.Repositories.Geolocation
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetRegionsPaged(TableMetadata? tableMetadata = null);
        Task<List<Region>> GetRegion();
        Task<int> GetRegionsCount();
        Task<Region> GetRegion(int id);
        Task InsertRegion (Region region);
        Task UpdateRegion (Region region);
        Task DeleteRegion (int id);
    }
}
