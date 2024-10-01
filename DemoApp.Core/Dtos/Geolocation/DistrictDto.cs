using DemoApp.Domain.Models.Geolocation;

namespace DemoApp.Core.Dtos.Geolocation
{
    public record DistrictDto
    {
        public int? DistrictId { get; set; }
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
                Name = districtDto.DistrictName
            };

            if (isUpdate && districtDto.DistrictId.HasValue)
            {
                district.Id = districtDto.DistrictId.Value;
                district.UserModified = Environment.UserDomainName;
                district.DateModified = DateTime.Now;
            }
            else 
            {
                district.UserCreated = Environment.UserDomainName;
                district.DateCreated = DateTime.Now;
            }
            
            return district;
        }
    }
}
