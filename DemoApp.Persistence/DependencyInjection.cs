using DemoApp.Domain.Interfaces.Repositories.Common;
using DemoApp.Domain.Interfaces.Repositories.Administration;
using DemoApp.Domain.Interfaces.Repositories.Geolocation;
using DemoApp.Persistence.Repositories.Administration;
using DemoApp.Persistence.Repositories.Common;
using DemoApp.Persistence.Repositories.Geolocation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdminDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AdminDbConnection"));
            }, ServiceLifetime.Scoped);

            services.AddDbContext<CoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CoreDbConnection"));
            }, ServiceLifetime.Scoped);

            services.AddAdministrationRepositories();
            services.AddCommonRepositories();
            services.AddGeolocationRepositories();

            return services;
        }

        private static void AddAdministrationRepositories(this IServiceCollection services)
        {
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<IAuthorizationRepository, AuthorizationRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        private static void AddCommonRepositories(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IGenderRepository, GenderRepository>();
            services.AddTransient<INationalIdTypeRepository, NationalIdTypeRepository>();
        }

        private static void AddGeolocationRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IDistrictRepository, DistrictRepository>();
            services.AddTransient<IPlaceRepository, PlaceRepository>();
            services.AddTransient<IRegionRepository, RegionRepository>();
        }
    }
}
