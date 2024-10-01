using DemoApp.Domain.CoreDbEntities;

namespace DemoApp.Core.Dtos.Geolocation
{
    public record CountryDto
    {
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }

        public static CountryDto CreateDto(Country country) 
        {
            return new CountryDto
            { 
                CountryId = country.Id, 
                CountryName = country.Name 
            };
        }

        public static Country ToModel(CountryDto countryDto, bool isUpdate = false) 
        {
            var country = new Country
            {
                Name = countryDto.CountryName
            };

            if (isUpdate && countryDto.CountryId.HasValue)
            {
                country.Id = countryDto.CountryId.Value;
                country.UserModified = Environment.UserDomainName;
                country.DateModified = DateTime.Now;
            }
            else
            {
                country.UserCreated = Environment.UserDomainName;
                country.DateCreated = DateTime.Now;
            }

            return country;
        }
    }
}
