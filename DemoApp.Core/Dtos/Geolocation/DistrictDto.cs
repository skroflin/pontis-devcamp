using DemoApp.Domain.Models.Geolocation;

namespace DemoApp.Core.Dtos.Geolocation
{
    public record DistrictDto
    {
        public int DistrictId { get; set; }
        public string? DistrictName { get; set; }

        public static DistrictDto CreateDto (District district)
        {
            return new DistrictDto
            {
                DistrictId = district.Id,
                DistrictName = district.Name
            };
        }

        public static District ToModel(DistrictDto districtDto, bool isUpdate = false)
        {
            var district = new District
            {
                Id = districtDto.DistrictId,
                Name = districtDto.DistrictName
            };

            if (isUpdate)
            {
                district.UserModified = Environment.UserDomainName;
                district.DateCreated = DateTime.Now;
            }
            else 
            {
                district.UserModified = Environment.UserDomainName;
                district.DateCreated = DateTime.Now;
            }
            
            return district;
        }
    }
}
