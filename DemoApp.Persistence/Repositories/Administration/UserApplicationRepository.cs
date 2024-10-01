using DemoApp.Domain.Interfaces.Repositories.Administration;
using DemoApp.Domain.Models.Administration;
using DemoApp.Domain.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories.Administration
{
    public class UserApplicationRepository : IUserApplicationRepository
    {
        private readonly AdminDbContext _context;
        public UserApplicationRepository(AdminDbContext context) 
        {
            _context = context;
        }
        public async Task DeleteUserApplication(int userId, int applicationId, int roleId)
        {
            var country = await _context.UserApplications
                .FirstOrDefaultAsync(ua => ua.UserId == userId
                                    && ua.ApplicationId == applicationId
                                    && ua.RoleId == roleId);
            if (country != null) 
            {
                _context.UserApplications.Remove(country);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserApplication> GetUserApplication(int userId, int applicationId, int roleId)
        {
            return await _context.UserApplications
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.ApplicationId == applicationId && ua.RoleId == roleId);
        }

        public async Task<List<UserApplication>> GetUserApplications()
        {
            return await _context.UserApplications.ToListAsync();
        }

        public async Task<int> GetUserApplicationsCount()
        {
            return await _context.UserApplications.CountAsync();
        }

        public async Task<List<UserApplication>> GetUserApplicationsPaged(TableMetadata? tableMetadata = null)
        {
            var pagedIndex = tableMetadata?.PagingMetadata.PageIndex == 0 ? 1 : tableMetadata.PagingMetadata.PageIndex;
            var pagingMetadata = tableMetadata.PagingMetadata;
            var query = _context.UserApplications
                .Skip((pagedIndex - 1) * pagingMetadata.PageSize)
                .Take(pagingMetadata.PageSize);
            return await query.ToListAsync();
        }

        public async Task InsertUserApplication(UserApplication User)
        {
            _context.UserApplications.Add(User);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserApplication(UserApplication userApplication)
        {
            var existingUserApplication = await _context.UserApplications
                .FirstOrDefaultAsync(ua => ua.UserId == userApplication.UserId
                                            && ua.ApplicationId == userApplication.ApplicationId
                                            && ua.RoleId == userApplication.RoleId);
            if (existingUserApplication != null)
            {
                _context.Entry(existingUserApplication).CurrentValues.SetValues(userApplication);
                await _context.SaveChangesAsync();
            }
        }
    }
}
