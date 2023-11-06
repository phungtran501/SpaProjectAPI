using SpaManagement.Domain.Entities;
using SpaManagement.Service.DTOs;

namespace SpaManagement.Service
{
    public interface IServicesService
    {
        Task<IEnumerable<ServiceDTO>> GetAllListServices();
    }
}