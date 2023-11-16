using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpaManagement.Service.Abstracts;

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
    }
}
