using DemoApp.Core.Dtos.Geolocation;
using DemoApp.Core.Services.Geolocation.Interfaces;
using DemoApp.Domain.Interfaces.Repositories.Geolocation;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Geolocation
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository) 
        {
            _countryRepository = countryRepository;
        }
        public async Task DeleteCountry(int id)
        {
            await _countryRepository.DeleteCountry(id);
        }

        public async Task<List<CountryDto>> GetCountries()
        {
            var countries = await _countryRepository.GetCountries();
            var countryDtos = new List<CountryDto>();
            foreach (var country in countries) 
            {
                countryDtos.Add(CountryDto.CreateDto(country));
            }
            return countryDtos;
        }

        public async Task<int> GetCountriesCount()
        {
            return await _countryRepository.GetCountryCount();
        }

        public async Task<CountryDto> GetCountry(int id)
        {
            var country = await _countryRepository.GetCountry(id);
            return CountryDto.CreateDto(country);
        }

        public async Task<PagedListDto<CountryDto>> GetCountryPaged(TableMetadata? tableMetadata)
        {
            var count = await _countryRepository.GetCountryCount();
            var countries = await _countryRepository.GetCountriesPaged(tableMetadata);
            var countryDtos = new List<CountryDto>();
            foreach(var country in countries)
            {
                countryDtos.Add(CountryDto.CreateDto(country));
            }
            var pagingMetadata = new PagingMetadata(count, tableMetadata.PagingMetadata);
            
            return new PagedListDto<CountryDto> { PagedData = countryDtos, PagingMetadata = pagingMetadata };
        }

        public async Task InsertCountry(CountryDto countryDto)
        {
            await _countryRepository.InsertCountry(CountryDto.ToModel(countryDto));
        }

        public async Task UpdateCountry(CountryDto countryDto)
        {
            await _countryRepository.UpdateCountry(CountryDto.ToModel(countryDto, true));
        }
    }
}
