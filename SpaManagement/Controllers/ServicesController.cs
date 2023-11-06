using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaManagement.Service;

namespace SpaManagement.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : Controller
    {
        private readonly IServicesService _servicesService;

        public ServicesController(IServicesService servicesService) 
        {
            _servicesService = servicesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var services = await _servicesService.GetAllListServices();

            return Ok(services);
        }
    }
}
