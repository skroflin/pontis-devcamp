using DemoApp.Core.Dtos.Geolocation;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Geolocation.Interfaces
{
    public interface IRegionService
    {
        Task<PagedListDto<RegionDto>> GetRegionPaged (TableMetadata? tableMetadata);
        Task<List<RegionDto>> GetRegions();
        Task<int> GetRegionsCount();
        Task<RegionDto> GetRegion(int id);
        Task InsertRegion (RegionDto regionDto);
        Task UpdateRegion(RegionDto regionDto);
        Task DeleteRegion (int id);
    }
}
