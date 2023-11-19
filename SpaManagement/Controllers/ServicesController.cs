using Microsoft.AspNetCore.Mvc;
using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Enums;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;

namespace SpaManagement.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : Controller
    {
        private readonly IServicesService _servicesService;
        private readonly IUnitOfWork _unitOfWork;


        public ServicesController(IServicesService servicesService, IUnitOfWork unitOfWork) 
        {
            _servicesService = servicesService;
            _unitOfWork = unitOfWork;

        }

        [HttpGet("get-list")]
        public async Task<IActionResult> Index(int page, int per_page)
        {
            var services = await _servicesService.GetAllListServices(page, per_page);

            return Ok(services);
        }

        [HttpGet("{id}/detail")]
        public async Task<IActionResult> InsertUpdate(int id)
        {

            var service = await _unitOfWork.ServicesRepository.GetById(id);

            var sv = new ServiceDTO
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Decription,
                IsActive = service.IsActive,
            };
            return Ok(sv);
        }

        [HttpPost("save")]
        public async Task<IActionResult> InsertUpdate([FromBody] ServiceDTO serviceDTO)
        {
            var result = await _servicesService.CreateUpdate(serviceDTO);
            if (result.Status && result.StatusType == StatusType.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _servicesService.DeleteService(id);
            return Json(true);
        }

        [HttpGet("get-services")]
        public async Task<IActionResult> GetServices()
        {
            var services = await _servicesService.GetServices();

            return Ok(services);
        }
    }
}
