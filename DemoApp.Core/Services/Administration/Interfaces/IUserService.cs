using DemoApp.Core.Dtos.Administration;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Administration.Interfaces
{
    public interface IUserService
    {
        Task<PagedListDto<UserDto>> GetUserPaged(TableMetadata? tableMetadata);
        Task<List<UserDto>> GetUsers();
        Task<int> GetUserCount();
        Task<UserDto> GetUser(int id);
        Task InsertUser(UserDto userDto);
        Task UpdateUser(UserDto userDto);
        Task DeleteUser(int id);
    }
}
