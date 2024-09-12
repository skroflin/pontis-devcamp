using DemoApp.Domain.Models.Common;

namespace DemoApp.Core.Dtos.Common
{
    public record NationalIdTypeDto
    {
        public int NationalIdTypeId { get; set; }
        public string? NationalIdTypeName { get; set; }

        public static NationalIdTypeDto CreateDto(NationalIdType nationalIdType)
        {
            return new NationalIdTypeDto
            {
                NationalIdTypeId = nationalIdType.Id,
                NationalIdTypeName = nationalIdType.Name
            };
        }

        public static NationalIdType ToModel(NationalIdTypeDto nationalIdTypeDto, bool isUpdate = false) 
        {
            var nationalIdType = new NationalIdType
            {
                Id = nationalIdTypeDto.NationalIdTypeId,
                Name = nationalIdTypeDto.NationalIdTypeName
            };

            return nationalIdType;
        }
    }
}
