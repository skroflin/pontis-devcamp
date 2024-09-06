using DemoApp.Domain.CoreDbEntities;
using DemoApp.Domain.Interfaces.Repositories.Geolocation;
using DemoApp.Domain.Models.Common;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Geolocation
{
    public class CountryRepository : ICountryRepository
    {
        private readonly CoreDbContext _context;
        public CountryRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetCountriesPaged(TableMetadata? tableMetadata)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.Countries
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);

            return await query.ToListAsync();
        }

        public async Task<List<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<int> GetCountryCount()
        {
            return await _context.Countries.CountAsync();
        }

        public async Task<Country> GetCountry(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task InsertCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCountry(Country country)
        {
            var existingCountry = await _context.Countries.FindAsync(country.Id);
            if (existingCountry != null)
            {
                _context.Entry(existingCountry).CurrentValues.SetValues(country);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
            }
        }
    }
}
