using DemoApp.Domain.Models.Common;

namespace DemoApp.Core.Dtos.Common
{
    public record GenderDto
    {
        public int GenderId { get; private set; }
        public string? GenderName { get; private set; }

        public static GenderDto CreateDto(Gender gender)
        {
            return new GenderDto 
            {
                GenderId = gender.Id,
                GenderName = gender.Name 
            };
        }

        public static Gender ToModel(GenderDto genderDto, bool isUpdate = false) 
        {
            var gender = new Gender
            {
                Id = genderDto.GenderId,
                Name = genderDto.GenderName
            };

            if (isUpdate)
            {
                gender.UserModified = Environment.UserDomainName;
                gender.DateCreated = DateTime.Now;
            }
            else 
            {
                gender.UserModified = Environment.UserDomainName;
                gender.DateCreated = DateTime.Now;
            }

            return gender;
        }
    }
}
