using DemoApp.Domain.Models.Administration;

namespace DemoApp.Core.Dtos.Administration
{
    public record AuthorizationDto
    {
        public int? AuthorizationId { get; set; }
        public string? AuthorizationName { get; set; }

        public static AuthorizationDto CreateDto(Authorization authorization)
        {
            return new AuthorizationDto
            {
                AuthorizationId = authorization.Id,
                AuthorizationName = authorization.Name
            };
        }

        public static Authorization ToModel(AuthorizationDto authorizationDto, bool isUpdate = false)
        {
            var authorization = new Authorization
            {
                Name = authorizationDto.AuthorizationName
            };

            if (isUpdate && authorizationDto.AuthorizationId.HasValue)
            {
                authorization.Id = authorizationDto.AuthorizationId.Value;
                authorization.UserModified = Environment.UserDomainName;
                authorization.DateModified = DateTime.Now;
            }
            else
            {
                authorization.UserCreated = Environment.UserDomainName;
                authorization.DateCreated = DateTime.Now;
            }

            return authorization;
        }
    }
}
