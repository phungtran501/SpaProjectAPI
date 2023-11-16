using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaManagement.Service.Abstracts;

namespace SpaManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PlanController : Controller
    {
        IPlanService _planService;
        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() //https://domain/api/menu
        {
            var plans = await _planService.GetPlan();
            return Ok(plans);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePlanAsync(int id)
        {
            return Ok(await _planService.UpdatePlan(id));
        }
    }
}
