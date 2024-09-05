using DemoApp.Core.Models.Geolocation;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Domain.Interfaces.Repositories.Geolocation
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetRegionsPaged(TableMetadata? tableMetadata = null);
        Task<List<Region>> GetRegions();
        Task<int> GetRegionsCount();
        Task<Region> GetRegion(int id);
        Task insertRegion (Region region);
        Task updateRegion (Region region);
        Task deleteRegion (Region region);
    }
}
