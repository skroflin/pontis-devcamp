using DemoApp.Domain.Interfaces.Repositories.Geolocation;
using DemoApp.Domain.Models.Geolocation;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Persistence.Repositories.Geolocation
{
    public interface DistrictRepository : IDistrictRepository
    {
        Task<List<District>> GetDistrictsPaged(TableMetadata? tableMetadata = null);
        Task<List<District>> GetDistricts();
        Task<int> GetDistrictCount();
        Task<District> GetDistrict(int id);
        Task InsertDistrict(District district);
        Task UpdateDistrict(District district);
        Task DeleteDistrict(int id);
    }
}
