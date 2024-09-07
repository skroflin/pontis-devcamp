using DemoApp.Domain.Interfaces.Repositories.Administration;
using DemoApp.Domain.Models.Administration;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Administration
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly AdminDbContext _context;
        public AuthorizationRepository(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<List<Authorization>> GetAuthorizationPaged(TableMetadata? tableMetadata)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.Authorizations
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);

            return await query.ToListAsync();
        }

        public async Task DeleteAuthorization(int id)
        {
            var country = await _context.Authorizations.FindAsync(id);
            if (country != null)
            {
                _context.Authorizations.Remove(country);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Authorization> GetAuthorization(int id)
        {
            return await _context.Authorizations.FindAsync(id);
        }

        public async Task<List<Authorization>> GetAuthorizations()
        {
            return await _context.Authorizations.ToListAsync();
        }

        public async Task<int> GetAuthorizationsCount()
        {
            return await _context.Authorizations.CountAsync();
        }

        public async Task InsertAuthorization(Authorization authorization)
        {
            _context.Authorizations.Add(authorization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthorization(Authorization authorization)
        {
            var existingCountry = await _context.Authorizations.FindAsync(authorization.Id);
            if (existingCountry != null)
            {
                _context.Entry(existingCountry).CurrentValues.SetValues(authorization);
                await _context.SaveChangesAsync();
            }
        }
    }
}
