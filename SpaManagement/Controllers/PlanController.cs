using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SpaManagement.Domain.Enums;
using SpaManagement.Service;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;

namespace SpaManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanController : Controller
    {
        IPlanService _planService; 
        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet("get-list")]
        public async Task<IActionResult> Index(int page, int per_page)
        {
            var plans = await _planService.GetListPlan(page, per_page);

            return Ok(plans);
        }

        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetPlanById(int id)
        {

            var plan = await _planService.GetPlanById(id);

            return Ok(plan);
        }

        [HttpPost("save")]
        public async Task<IActionResult> InsertUpdate([FromBody] PlanDTO md)
        {
            var result = await _planService.CreateUpdate(md);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _planService.DeletePlan(id);
            return Ok(true);
        }
    }
}
 