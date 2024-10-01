using DemoApp.Core.Dtos.Administration;
using DemoApp.Core.Services.Administration.Interfaces;
using DemoApp.Domain.Interfaces.Repositories.Administration;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Administration
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        public async Task<UserDto> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            return UserDto.CreateDto(user);
        }

        public async Task<int> GetUserCount()
        {
            return await _userRepository.GetUsersCount();
        }

        public async Task<PagedListDto<UserDto>> GetUserPaged(TableMetadata? tableMetadata)
        {
            var count = await _userRepository.GetUsersCount();
            var users = await _userRepository.GetUsersPaged(tableMetadata);
            var userDtos = new List<UserDto>();
            foreach (var user in users) 
            {
                userDtos.Add(UserDto.CreateDto(user));
            }
            var pagingMetadata = new PagingMetadata(count, tableMetadata.PagingMetadata);

            return new PagedListDto<UserDto> { PagedData = userDtos, PagingMetadata = pagingMetadata };
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var roles = await _userRepository.GetUsers();
            var userDtos = new List<UserDto>();
            foreach (var role in roles) 
            {
                userDtos.Add(UserDto.CreateDto(role));
            }
            return userDtos;

        }

        public async Task InsertUser(UserDto userDto)
        {
            await _userRepository.InsertUser(UserDto.ToModel(userDto));
        }

        public async Task UpdateUser(UserDto userDto)
        {
            await _userRepository.UpdateUser(UserDto.ToModel(userDto, true));
        }
    }
}
