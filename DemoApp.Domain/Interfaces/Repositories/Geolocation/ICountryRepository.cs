using DemoApp.Domain.CoreDbEntities;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Domain.Interfaces.Repositories.Geolocation
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountriesPaged(TableMetadata? tableMetadata = null);
        Task<List<Country>> GetCountries();
        Task<int> GetCountryCount();
        Task<Country> GetCountry(int id);
        Task InsertCountry (Country country);
        Task UpdateCountry (Country country);
        Task DeleteCountry (int id);
    }
}
