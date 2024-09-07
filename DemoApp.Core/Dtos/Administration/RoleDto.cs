using DemoApp.Domain.Models.Administration;

namespace DemoApp.Core.Dtos.Administration
{
    public record RoleDto
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public static RoleDto CreateDto(Role role)
        {
            return new RoleDto
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
        }

        public static Role ToModel(RoleDto roleDto, bool isUpdate = false)
        {
            var role = new Role
            {
                Id = roleDto.RoleId,
                Name = roleDto.RoleName
            };

            if (isUpdate)
            {
                role.UserModified = Environment.UserDomainName;
                role.DateCreated = DateTime.Now;
            }
            else
            {
                role.UserModified = Environment.UserDomainName;
                role.DateCreated = DateTime.Now;
            }
            
            return role;
        }
    }
}
