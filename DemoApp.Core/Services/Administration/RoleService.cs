using DemoApp.Core.Dtos.Administration;
using DemoApp.Core.Services.Administration.Interfaces;
using DemoApp.Domain.Interfaces.Repositories.Administration;
using DemoApp.Domain.Paging;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Core.Services.Administration
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<PagedListDto<RoleDto>> GetRolePaged(TableMetadata? tableMetadata)
        {
            var count = await _roleRepository.GetRolesCount();
            var roles = await _roleRepository.GetRolesPaged(tableMetadata);
            var roleDtos = new List<RoleDto>();
            foreach (var role in roles)
            {
                roleDtos.Add(RoleDto.CreateDto(role));
            }
            var pagingMetadata = new PagingMetadata(count, tableMetadata.PagingMetadata);

            return new PagedListDto<RoleDto>() { PagedData = roleDtos, PagingMetadata = pagingMetadata };
        }

        public async Task DeleteRole(int id)
        {
            await _roleRepository.DeleteRole(id);
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetRoles();
            var roleDtos = new List<RoleDto>();
            foreach(var role in roles)
            {
                roleDtos.Add(RoleDto.CreateDto(role));
            }
            return roleDtos;

        }

        public async Task<int> GetRolesCount()
        {
            return await _roleRepository.GetRolesCount();
        }

        public async Task<RoleDto> GetRole(int id)
        {
            var role = await _roleRepository.GetRole(id);
            return RoleDto.CreateDto(role);
        }

        public async Task InsertRole(RoleDto roleDto)
        {
            await _roleRepository.InsertRole(RoleDto.ToModel(roleDto));
        }

        public async Task UpdateRole(RoleDto roleDto)
        {
            await _roleRepository.UpdateRole(RoleDto.ToModel(roleDto, true));
        }
    }
}
