using SpaManagement.Service.DTOs;
using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Domain.Enums;
using SpaManagement.Service.Abstracts;
using SpaManagement.Domain.Helper;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace SpaManagement.Service
{
    public class ServicesService : IServicesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServicesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ServiceDTO>> GetAllListServices(int pageIndex, int pageSize)
        {
            var services = await _unitOfWork.ServicesRepository.GetData(sv => sv.IsActive);

            var result = services.Skip((pageIndex - 1) * pageSize).Take(pageIndex * pageSize).ToList();

            int total = services.Count();

            var data = result.Select(x => new ServiceDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Decription.Length>100 ? x.Decription.Substring(0, 100) : x.Decription,
            });

            return data;
        }

        public async Task<ResponseModel> CreateUpdate(ServiceDTO serviceDTO)
        {
            var service = await _unitOfWork.ServicesRepository.GetSingleByConditionAsync(x => x.Name.ToLower() == serviceDTO.Name.ToLower() 
                                                                                   && x.Id != serviceDTO.Id);
            if (service is not null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Service name is exist",
                    StatusType = StatusType.Fail
                };
            }

            var maxId = serviceDTO.Id;

            if (serviceDTO.Id == 0)
            {
                var ser = new Services
                {
                    Name = serviceDTO.Name,
                    IsActive = serviceDTO.IsActive,
                    Decription = serviceDTO.Description
                };

                await _unitOfWork.ServicesRepository.Insert(ser);
                await _unitOfWork.ServicesRepository.Commit();
                maxId = ser.Id;
            }
            else  
            {
                var ser = await _unitOfWork.ServicesRepository.GetById(serviceDTO.Id);
                ser.Name = serviceDTO.Name;
                ser.IsActive = serviceDTO.IsActive;
                ser.Decription = serviceDTO.Description;

                _unitOfWork.ServicesRepository.Update(ser);
                await _unitOfWork.ServicesRepository.Commit();
            }
       
            return new ResponseModel
            {
                Status = true,
                Message = serviceDTO.Id is null ? "" : "Insert successful",
                StatusType = StatusType.Success,
                Data = maxId
            };
        }

        public async Task DeleteService(int key)
        {
            var service = await _unitOfWork.ServicesRepository.GetById(key);
            service.IsActive = false;
            _unitOfWork.ServicesRepository.Update(service);
            await _unitOfWork.ServicesRepository.Commit();
        }

        public async Task<IEnumerable<ServiceResponse>> GetServices()
        {
            var services = await _unitOfWork.ServicesRepository.GetData(x => x.IsActive);

            var result = services.Select(x => new ServiceResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Decription.Length > 100 ? x.Decription.Substring(0, 100) : x.Decription,
            }).ToList() ;

            return result;
        }

        public async Task<IEnumerable<ServiceResponse>> GetRandomServices()
        {
            var services = await _unitOfWork.ServicesRepository.GetData(x => x.IsActive);

            var result = services.Select(x => new ServiceResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Decription.Length > 100 ? x.Decription.Substring(0, 100) : x.Decription,
            }).ToList();

            var random = result.OrderBy(x => Guid.NewGuid()).Take(3).ToList();

            return random;
        }

    }
}
