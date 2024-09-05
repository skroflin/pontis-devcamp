﻿using DemoApp.Core.Models.Administration;
using DemoApp.Domain.Paging.Models;

namespace DemoApp.Persistence.Repositories.Administration
{
    public interface RoleRepository
    {
        Task<List<Role>> GetRolesPaged(TableMetadata? tableMetadata = null);
        Task<List<Role>> GetRoles();
        Task<int> GetRolesCount();
        Task<Role> GetRole(int id);
        Task InsertRole(Role role);
        Task UpdateRole(Role role);
        Task DeleteRole(int id);
    }
}
