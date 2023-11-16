using SpaManagement.Domain.Entities;
using SpaManagement.Service.DTOs;

namespace SpaManagement.Service.Abstracts
{
    public interface IServicesService
    {
        Task<ResponseModel> CreateUpdate(ServiceDTO serviceDTO);
        Task DeleteService(int key);
        Task<IEnumerable<ServiceDTO>> GetAllListServices(int page, int per_page);
        Task<IEnumerable<ServiceDTO>> GetServices();
    }
}