using DemoApp.Domain.Models.Administration;

namespace DemoApp.Core.Dtos.Administration
{
    public record ApplicationDto
    {
        public int? ApplicationId { get; set; }
        public string? ApplicationName { get; set; }

        public static ApplicationDto CreateDto(Application application)
        {
            return new ApplicationDto
            {
                ApplicationId = application.Id,
                ApplicationName = application.Name
            };
        }

        public static Application ToModel(ApplicationDto applicationDto, bool isUpdate = false)
        {
            var application = new Application
            {
                Name = applicationDto.ApplicationName
            };

            if (isUpdate && applicationDto.ApplicationId.HasValue)
            {
                application.Id = applicationDto.ApplicationId.Value;
                application.UserModified = Environment.UserDomainName;
                application.DateModified = DateTime.Now;
            }
            else
            {
                application.UserCreated = Environment.UserDomainName;
                application.DateCreated = DateTime.Now;
            }

            return application;
        }
    }
}
