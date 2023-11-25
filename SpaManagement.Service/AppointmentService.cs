using Dapper;
using Microsoft.AspNetCore.Identity;
using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Domain.Enums;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;
using System.Data;

namespace SpaManagement.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperHelper _dapperHelper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentService(IUnitOfWork unitOfWork, IDapperHelper dapperHelper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _dapperHelper = dapperHelper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAllAppointment(int pageIndex, int pageSize)
        {

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("pageIndex", pageIndex, DbType.Int32, ParameterDirection.Input);
            dynamicParameters.Add("pageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            dynamicParameters.Add("totalRecord", 0, DbType.Int32, ParameterDirection.Output);

            var result = await _dapperHelper.ExcuteStoreProcedureReturnList<AppointmentDTO>("GetAllAppointment", dynamicParameters);

            var total = dynamicParameters.Get<int>("totalRecord");

            var data = result.Select(x => new AppointmentDTO
            {
                Id = x.Id,
                UserName = x.UserName,
                Note = x.Note,
                CreatedOn = x.CreatedOn,
                Status = GetStatusAppointment(Convert.ToInt16(x.Status))

            }).ToArray();

            return data;
        }

        public async Task<AppoinmentRequestDTO> GetAppointmentById(int id)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetById(id);

            return new AppoinmentRequestDTO
                {
                    Id = appointment.Id,
                    UserId = appointment.UserId,
                    Note = appointment.Note,
                    Status = appointment.Status,
                };
        }

        private string GetStatusAppointment(short status)
        {
            switch (status)
            {
                case (short)StatusAppointment.Confirmed:
                    return nameof(StatusAppointment.Confirmed);
                case (short)StatusAppointment.Completed: 
                    return nameof(StatusAppointment.Completed);
                case (short)StatusAppointment.Cancelled: 
                    return nameof(StatusAppointment.Cancelled);
                default:
                    return default;
            }

        }
        public async Task<ResponseModel> CreateUpdate(AppoinmentRequestDTO appoinmentRequestDTO)
        {

            if (appoinmentRequestDTO.Id == 0)
            {
                var app = new Appointment
                {
                    UserId = appoinmentRequestDTO.UserId,
                    CreatedOn = DateTime.Now,
                    Note = appoinmentRequestDTO.Note,
                    Status = appoinmentRequestDTO.Status,
                };
                var result = _unitOfWork.AppointmentRepository.Insert(app);

            }
            //update
            else
            {
                var app = await _unitOfWork.AppointmentRepository.GetById(appoinmentRequestDTO.Id);

                app.UserId = appoinmentRequestDTO.UserId;
                app.Note = appoinmentRequestDTO.Note;
                app.Status = appoinmentRequestDTO.Status;
                app.CreatedOn = DateTime.Now;

                _unitOfWork.AppointmentRepository.Update(app);

            }
            await _unitOfWork.AppointmentRepository.Commit();

            return new ResponseModel
            {
                Status = true,
                Message = appoinmentRequestDTO.Id is null ? "Insert successful" : "Update successful",
                StatusType = StatusType.Success,
             
            };

        }

        public async Task DeleteAppointment(int id)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetById(id);
            _unitOfWork.AppointmentRepository.Delete(appointment);
            await _unitOfWork.AppointmentRepository.Commit();
        }
    }
}
