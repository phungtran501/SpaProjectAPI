using Microsoft.AspNetCore.Mvc;
using SpaManagement.Domain.Enums;
using SpaManagement.Service;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;

namespace SpaManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService) 
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRoles();

            return Ok(roles);
        }

        [HttpGet("get-list")]
        public async Task<IActionResult> AllRole(int page, int per_page)
        {
            var roles = await _roleService.GetListRole(page, per_page);

            return Ok(roles);
        }

        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetRoleDetail(string id)
        {

            var role = await _roleService.GetRoleById(id);

            return Ok(role);
        }


        [HttpPost("save")]
        public async Task<IActionResult> InsertUpdate([FromBody] RoleDTO roleDTO)
        {
            var result = await _roleService.CreateUpdate(roleDTO);

                return Ok(result.Message);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(string id)
        {
            await _roleService.DeleteRole(id);

            return Ok(true);
        }
    }
}
