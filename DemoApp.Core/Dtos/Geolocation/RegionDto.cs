using DemoApp.Domain.Models.Geolocation;

namespace DemoApp.Core.Dtos.Geolocation
{
    public record RegionDto
    {
        public int? RegionId { get; set; }
        public string? RegionName { get; set; }

        public static RegionDto CreateDto(Region region)
        {
            return new RegionDto
            {
                RegionId = region.Id,
                RegionName = region.Name
            };
        }

        public static Region ToModel(RegionDto regionDto, bool isUpdate = false)
        {
            var region = new Region
            {
                Name = regionDto.RegionName
            };
            
            if (isUpdate && regionDto.RegionId.HasValue)
            {
                region.Id = regionDto.RegionId.Value;
                region.UserModified = Environment.UserDomainName;
                region.DateModified = DateTime.Now;
            }
            else
            {
                region.UserCreated = Environment.UserDomainName;
                region.DateCreated = DateTime.Now;
            }

            return region;
        }
    }
}
