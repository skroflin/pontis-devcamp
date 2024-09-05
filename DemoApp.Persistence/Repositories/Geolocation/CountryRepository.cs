using DemoApp.Core.CoreDbEntities;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Persistence.Repositories.Geolocation
{
    public interface CountryRepository
    {
        Task<List<Country>> GetCountriesPaged(TableMetadata tableMetadata);
        Task<List<Country>> GetCountries();
        Task<int> GetCountryCount();
        Task<Country> GetCountry(int id);
        Task InsertCountry (Country country);
        Task UpdateCountry (Country country);
        Task DeleteCountry (int id);
    }
}
