using DemoApp.Domain.Interfaces.Repositories.Administration;
using DemoApp.Domain.Models.Administration;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Administration
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AdminDbContext _context;
        public ApplicationRepository(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<List<Application>> GetApplicationsPaged(TableMetadata? tableMetadata)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.Applications
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);

            return await query.ToListAsync();
        }

        public async Task<List<Application>> GetApplications()
        {
            return await _context.Applications.ToListAsync();
        }


        public async Task<int> GetApplicationsCount()
        {
            return await _context.Applications.CountAsync();
        }

        public async Task<Application> GetApplication(int id)
        {
            return await _context.Applications.FindAsync(id);
        }

        public async Task InsertApplication(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateApplication(Application application)
        {
            var existingCountry = await _context.Applications.FindAsync(application.Id);
            if (existingCountry != null)
            {
                _context.Entry(existingCountry).CurrentValues.SetValues(application);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteApplication(int id)
        {
            var country = await _context.Applications.FindAsync(id);
            if (country != null)
            {
                _context.Applications.Remove(country);
                await _context.SaveChangesAsync();
            }
        }
    }
}
