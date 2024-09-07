using DemoApp.Domain.Models.Administration;

namespace DemoApp.Core.Dtos.Administration
{
    public record ApplicationDto
    {
        public int ApplicationId { get; set; }
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
                Id = applicationDto.ApplicationId,
                Name = applicationDto.ApplicationName
            };

            if (isUpdate)
            {
                application.UserModified = Environment.UserDomainName;
                application.DateCreated = DateTime.Now;
            }
            else
            {
                application.UserModified = Environment.UserDomainName;
                application.DateCreated = DateTime.Now;
            }

            return application;
        }
    }
}
