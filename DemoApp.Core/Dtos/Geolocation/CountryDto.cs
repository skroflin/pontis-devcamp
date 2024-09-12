using DemoApp.Domain.CoreDbEntities;

namespace DemoApp.Core.Dtos.Geolocation
{
    public record CountryDto
    {
        public int CountryId { get; set; }
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
                Id = countryDto.CountryId,
                Name = countryDto.CountryName
            };

            if (isUpdate)
            {
                country.UserModified = Environment.UserDomainName;
                country.DateCreated = DateTime.Now;
            }
            else
            {
                country.UserModified = Environment.UserDomainName;
                country.DateCreated = DateTime.Now;
            }

            return country;
        }
    }
}
