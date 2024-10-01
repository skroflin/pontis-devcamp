using DemoApp.Domain.Interfaces.Repositories.Common;
using DemoApp.Domain.Models.Common;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Common
{
    public class NationalIdTypeRepository : INationalIdTypeRepository
    {
        private readonly CoreDbContext _context;
        public NationalIdTypeRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<NationalIdType>> GetNationalIdTypesPaged(TableMetadata? tableMetadata)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.NationalIdTypes
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);

            return await query.ToListAsync();
        }

        public async Task<List<NationalIdType>> GetNationalIdTypes()
        {
            return await _context.NationalIdTypes.ToListAsync();
        }

        public async Task<int> GetNationalIdTypesCount()
        {
            return await _context.NationalIdTypes.CountAsync();
        }

        public async Task<NationalIdType> GetNationalIdType(int id)
        {
            return await _context.NationalIdTypes.FindAsync(id);
        }

        public async Task InsertNationalIdType(NationalIdType nationalIdType)
        {
            _context.NationalIdTypes.Add(nationalIdType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNationalIdType(NationalIdType nationalIdType)
        {
            var existingCountry = await _context.NationalIdTypes.FindAsync(nationalIdType.Id);
            if (existingCountry != null)
            {
                _context.Entry(existingCountry).CurrentValues.SetValues(nationalIdType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteNationalIdType(int id)
        {
                var country = await _context.NationalIdTypes.FindAsync(id);
                if (country != null)
                {
                    _context.NationalIdTypes.Remove(country);
                    await _context.SaveChangesAsync();
                }
            }
    }
}
