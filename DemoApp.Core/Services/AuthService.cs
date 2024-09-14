using DemoApp.Core.Dtos;
using DemoApp.Domain.Models.Administration;
using DemoApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Core.Services
{
    public class AuthService
    {
        private readonly AdminDbContext _context;

        public AuthService(AdminDbContext context)
        {
            _context = context;
        }

        public async Task<AuthResponseDto> AuthenticateAsync(string username, string password, string applicationName)
        {
            var user = await _context.Users
                .Include(u => u.UserApplications)
                    .ThenInclude(ua => ua.Role)
                .Include(u => u.UserApplications)
                    .ThenInclude(ua => ua.Application)
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password &&
                    u.UserApplications.Any(ua => ua.Application.Name == applicationName));

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials or application name.");
            }

            var userApplication = user.UserApplications
                .FirstOrDefault(ua => ua.Application.Name == applicationName);

            if (userApplication == null)
            {
                throw new UnauthorizedAccessException("User is not assigned to this application.");
            }

            var userRole = userApplication.Role;

            var roleAuthorizations = await _context.RoleAuthorizations
                .Where(ra => ra.RoleId == userRole.Id)
                .Select(ra => ra.Authorization.Name)
                .ToListAsync();

            return new AuthResponseDto
            {
                Username = user.Username,
                UserRole = userRole.Name,
                RoleAuthorizations = roleAuthorizations
            };
        }
    }

}
