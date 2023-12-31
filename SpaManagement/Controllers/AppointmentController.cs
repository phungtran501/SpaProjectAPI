﻿using Microsoft.AspNetCore.Mvc;
using SpaManagement.Domain.Enums;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;

namespace SpaManagement.Controllers
{

    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("get-list")]
        public async Task<IActionResult> Index(int page, int per_page)
        {
            var appointments = await _appointmentService.GetAllAppointment(page, per_page);

            return Ok(appointments);
        }

        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetAppointmentDetail(int id)
        {

            var appointment = await _appointmentService.GetAppointmentById(id);

            return Ok(appointment);
        }

        [HttpPost("save")]
        public async Task<IActionResult> InsertUpdate([FromBody] AppoinmentRequestDTO appoinmentRequestDTO)
        {
            var result = await _appointmentService.CreateUpdate(appoinmentRequestDTO);

                return Ok(result.Message);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await _appointmentService.DeleteAppointment(id);
            return Ok(true);
        }
    }
}
