using DemoApp.Domain.Models.Administration;

namespace DemoApp.Core.Dtos.Administration
{
    public record UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public static UserDto CreateDto(User user)
        {
            return new UserDto 
            { 
                UserId = user.Id, 
                UserName = user.Username 
            };
        }

        public static User ToModel(UserDto userDto, bool isUpdate = false) 
        {
            var user = new User
            {
                Id = userDto.UserId,
                Username = userDto.UserName
            };
            if (isUpdate) 
            {
                user.UserModified = Environment.UserDomainName;
                user.DateCreated = DateTime.Now;
            }
            else
            {
                user.UserModified = Environment.UserDomainName;
                user.DateCreated = DateTime.Now;
            }
            return user;
        }
    }
}
