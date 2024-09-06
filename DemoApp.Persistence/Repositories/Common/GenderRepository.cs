using DemoApp.Domain.Interfaces.Repositories.Common;
using DemoApp.Domain.Models.Common;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Common
{
    public class GenderRepository : IGenderRepository
    {
        private readonly CoreDbContext _context;
        public GenderRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Gender>> GetGendersPaged(TableMetadata? tableMetadata)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.Genders
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);

            return await query.ToListAsync();
        }

        public async Task<List<Gender>> GetGenders()
        {
            return await _context.Genders.ToListAsync();
        }

        public async Task<int> GetGendersCount()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task<Gender> GetGender(int id)
        {
            return await _context.Genders.FindAsync(id);
        }

        public async Task InsertGender(Gender gender)
        {
            _context.Genders.Add(gender);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGender(Gender gender)
        {
            var existingCountry = await _context.Genders.FindAsync(gender.Id);
            if (existingCountry != null)
            {
                _context.Entry(existingCountry).CurrentValues.SetValues(gender);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGender(int id)
        {
            var country = await _context.Genders.FindAsync(id);
            if (country != null)
            {
                _context.Genders.Remove(country);
                await _context.SaveChangesAsync();
            }
        }
    }
}
