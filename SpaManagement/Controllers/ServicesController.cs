﻿using Microsoft.AspNetCore.Mvc;
using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Enums;
using SpaManagement.Domain.Helper;
using SpaManagement.Service;
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
        private readonly IImageHandler _imageHandler;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServicesController(IServicesService servicesService, 
            IUnitOfWork unitOfWork, 
            IImageHandler imageHandler,
            IWebHostEnvironment  webHostEnvironment) 
        {
            _servicesService = servicesService; 
            _unitOfWork = unitOfWork;
            _imageHandler = imageHandler;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> InsertUpdate([FromForm] ServiceDTO md)
        {

            var result = await _servicesService.CreateUpdate(md);
            if (result.Status && result.StatusType == StatusType.Success)
            {
                var rootPath = _webHostEnvironment.WebRootPath;
                var path = Path.Combine(rootPath, "Image/service");
                var id = (int)result.Data;
                await _imageHandler.SaveImage(path, new List<IFormFile> { md.Image }, $"{id}.png");

                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await _servicesService.DeleteService(id);
            return Ok(true);
        }

        [HttpGet("get-services")]
        public async Task<IActionResult> GetServices()
        {
            var services = await _servicesService.GetServices();

            return Ok(services);
        }

        [HttpGet("random-service")]
        public async Task<IActionResult> RandomService()
        {
            var services = await _servicesService.GetRandomServices();

            return Ok(services);
        }
    }
}
