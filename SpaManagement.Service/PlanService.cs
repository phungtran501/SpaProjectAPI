using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Service.Abstracts;

namespace SpaManagement.Service
{
    public class PlanService : IPlanService
    {
        IUnitOfWork _unitOfWork;
        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Plan>> GetPlan()
        {
            var plans = await _unitOfWork.PlanRepository.GetData(plan => plan.IsActive);

            return plans;
        }

        public async Task<bool> UpdatePlan(int id)
        {
            var menu = await _unitOfWork.PlanRepository.GetById(id);

            if(menu is null)
            {
                //return NotFound("")
            }

            menu.IsActive = false;

            await _unitOfWork.PlanRepository.Commit();
            return await Task.FromResult(true);
        }
    }
}
