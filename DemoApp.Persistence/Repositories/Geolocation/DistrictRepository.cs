using DemoApp.Domain.Interfaces.Repositories.Geolocation;
using DemoApp.Domain.Models.Geolocation;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Geolocation
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly CoreDbContext _context;
        public DistrictRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<District>> GetDistrictsPaged(TableMetadata? tableMetadata)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.Districts
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);

            return await query.ToListAsync();
        }

        public async Task<List<District>> GetDistricts()
        {
            return await _context.Districts.ToListAsync();
        }

        public async Task<int> GetDistrictCount()
        {
            return await _context.Districts.CountAsync();
        }

        public async Task<District> GetDistrict(int id)
        {
            return await _context.Districts.FindAsync(id);
        }

        public async Task insertDistrict(District district)
        {
            _context.Districts.Add(district);
            await _context.SaveChangesAsync();
        }

        public async Task updateDistrict(District district)
        {
            var existingCountry = await _context.Districts.FindAsync(district.Id);
            if (existingCountry != null)
            {
                _context.Entry(existingCountry).CurrentValues.SetValues(district);
                await _context.SaveChangesAsync();
            }
        }

        public async Task deleteDistrict(int id)
        {
            var country = await _context.Districts.FindAsync(id);
            if (country != null)
            {
                _context.Districts.Remove(country);
                await _context.SaveChangesAsync();
            }
        }
    }
}
