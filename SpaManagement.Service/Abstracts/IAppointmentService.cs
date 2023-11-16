using SpaManagement.Service.DTOs;

namespace SpaManagement.Service.Abstracts
{
    public interface IAppointmentService
    {
        Task<ResponseModel> CreateUpdate(AppoinmentRequestDTO appoinmentRequestDTO);
        Task DeleteAppointment(int id);
        Task<IEnumerable<AppointmentDTO>> GetAllAppointment(int pageIndex, int pageSize);
        Task<AppoinmentRequestDTO> GetAppointmentById(int id);
    }
}