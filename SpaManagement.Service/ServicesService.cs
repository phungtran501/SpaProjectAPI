using SpaManagement.Service.DTOs;
using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;

namespace SpaManagement.Service
{
    public class ServicesService : IServicesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServicesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ServiceDTO>> GetAllListServices()
        {
            var services = await _unitOfWork.ServicesRepository.GetData(sv => sv.IsActive);

            var result = services.Select(x => new ServiceDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Decription
            });

            return result;
        }
    }
}
