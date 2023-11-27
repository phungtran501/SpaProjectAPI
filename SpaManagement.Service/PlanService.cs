using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Domain.Enums;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;
using SpaManagement.Service.DTOs.Product;

namespace SpaManagement.Service
{
    public class PlanService : IPlanService
    {
        IUnitOfWork _unitOfWork;
        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PlanDTO>> GetListPlan(int pageIndex, int pageSize)
        {
            var plans = await _unitOfWork.PlanRepository.GetData(x => x.IsActive);

            var result = plans.Skip((pageIndex - 1) * pageSize).Take(pageIndex * pageSize).ToList();

            int total = plans.Count();

            var data = result.Select(x => new PlanDTO
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Decription.Length > 100 ? x.Decription.Substring(0, 100) : x.Decription,
            });

            return data;
        }

        public async Task<ResponseModel> CreateUpdate(PlanDTO planDTO)
        {
            var plan = await _unitOfWork.PlanRepository.GetSingleByConditionAsync(x => x.Name.ToLower() == planDTO.Name.ToLower()
                                                                                   && x.Id != planDTO.Id);

            if (plan is not null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Plan name is exist",
                    StatusType = StatusType.Fail,
                };
            }

            if (planDTO.Id == 0)
            {
                var newPlan = new Plan
                {
                    Name = planDTO.Name,
                    Decription = planDTO.Description,
                    Price = planDTO.Price,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };

                var result = _unitOfWork.PlanRepository.Insert(newPlan);
            }
            //update
            else
            {
                var getPlan = await _unitOfWork.PlanRepository.GetById(planDTO.Id);

                getPlan.Name = planDTO.Name;
                getPlan.Decription = planDTO.Description;
                getPlan.Price = planDTO.Price;
                getPlan.CreatedOn = DateTime.Now;

                _unitOfWork.PlanRepository.Update(getPlan);

            }
            await _unitOfWork.PlanRepository.Commit();

            return new ResponseModel
            {
                Status = true,
                Message = planDTO.Id is null ? "Insert successful" : "Update successful",
                StatusType = StatusType.Success,
            };

        }

        public async Task<PlanDTO> GetPlanById(int id)
        {

            var plan = await _unitOfWork.PlanRepository.GetById(id);

            var result = new PlanDTO
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Decription,
                Price= plan.Price,
                IsActive = plan.IsActive,
            };

            return result;
        }

        public async Task DeletePlan(int key)
        {
            var plan = await _unitOfWork.PlanRepository.GetById(key);
            plan.IsActive = false;
            _unitOfWork.PlanRepository.Update(plan);
            await _unitOfWork.PlanRepository.Commit();
        }
    }
}
