using DemoApp.Core.Dtos.Administration;
using DemoApp.Core.Services.Administration.Interfaces;
using DemoApp.Domain.Paging.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.api.Controllers.Administration
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService role)
        {
            _roleService = role;
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetRolesPaged([FromBody] TableMetadata? tableMetadata)
        {
            var result = await _roleService.GetRolePaged(tableMetadata);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _roleService.GetRoles();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            var result = await _roleService.GetRole(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRole([FromBody] RoleDto roleDto)
        {
            await _roleService.InsertRole(roleDto);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateRole([FromBody] RoleDto roleDto)
        {
            await _roleService.UpdateRole(roleDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            await _roleService.DeleteRole(id);
            return Ok();
        }
    }
}
