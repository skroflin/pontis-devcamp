using DemoApp.Core.CoreDbEntities;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Interfaces.Repositories.Geolocation
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountriesPaged(TableMetadata? tableMetadata = null);
        Task<List<Country>> GetCountries();
        Task<int> GetCountryCount();
        Task<Country> GetCountry(int id);
        Task insertCountry (Country country);
        Task updateCountry (Country country);
        Task deleteCountry (int id);
    }
}
