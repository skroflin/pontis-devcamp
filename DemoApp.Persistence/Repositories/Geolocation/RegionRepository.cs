using DemoApp.Domain.Interfaces.Repositories.Geolocation;
using DemoApp.Domain.Models.Geolocation;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Geolocation
{
    public class RegionRepository : IRegionRepository
    {
        private readonly CoreDbContext _context;
        public RegionRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Region>> GetRegionsPaged(TableMetadata? tableMetadata)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.Regions
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);

            return await query.ToListAsync();
        }

        public async Task<List<Region>> GetRegions()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<int> GetRegionsCount()
        {
            return await _context.Regions.CountAsync();
        }

        public async Task<Region> GetRegion(int id)
        {
            return await _context.Regions.FindAsync(id);
        }

        public async Task InsertRegion(Region region)
        {
            _context.Regions.Add(region);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRegion(Region region)
        {
            var existingCountry = await _context.Places.FindAsync(region.Id);
            if (existingCountry != null)
            {
                _context.Entry(existingCountry).CurrentValues.SetValues(region);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRegion(int id)
        {
            var country = await _context.Regions.FindAsync(id);
            if (country != null)
            {
                _context.Regions.Remove(country);
                await _context.SaveChangesAsync();
            }
        }
    }
}
