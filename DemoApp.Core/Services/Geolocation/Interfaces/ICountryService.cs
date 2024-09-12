using DemoApp.Core.Dtos.Geolocation;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Geolocation.Interfaces
{
    public interface ICountryService
    {
        Task<PagedListDto<CountryDto>> GetCountryPaged(TableMetadata? tableMetadata);
        Task<List<CountryDto>> GetCountries();
        Task<int> GetCountriesCount();
        Task<CountryDto> GetCountry(int id);
        Task InsertCountry (CountryDto countryDto);
        Task UpdateCountry (CountryDto countryDto);
        Task DeleteCountry (int id);
    }
}
