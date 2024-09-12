using DemoApp.Core.Service.Administration;
using DemoApp.Core.Services.Administration;
using DemoApp.Core.Services.Administration.Interfaces;
using DemoApp.Core.Services.Common;
using DemoApp.Core.Services.Common.Interfaces;
using DemoApp.Core.Services.Geolocation;
using DemoApp.Core.Services.Geolocation.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAdministrationServices();
            services.AddCommonServices();
            services.AddGeolocationServices();
            return services;
        }

        private static void AddAdministrationServices(this IServiceCollection services) 
        {
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();
        }

        private static void AddCommonServices(this IServiceCollection services) 
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IGenderService, GenderService>();
            services.AddTransient<INationalIdTypeService, NationalIdTypeService>();
        }

        private static void AddGeolocationServices(this IServiceCollection services) 
        {
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IDistrictService, IDistrictService>();
            services.AddTransient<IPlaceService, PlaceService>();
            services.AddTransient<IRegionService, RegionService>();
        }
    }
}
