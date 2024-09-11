using DemoApp.Core.Dtos.Geolocation;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Geolocation.Interfaces
{
    public interface IDistrictService
    {
        Task<PagedListDto<DistrictDto>> GetDistrictPaged (TableMetadata? tableMetadata);
        Task<List<DistrictDto>> GetDistricts();
        Task<int> GetDistrictsCount();
        Task<DistrictDto> GetDistrict(int id);
        Task InsertDistrict (DistrictDto districtDto);
        Task UpdateDistrict (DistrictDto districtDto);
        Task DeleteDistrict (int id);
    }
}
