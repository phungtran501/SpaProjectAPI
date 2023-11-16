using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            menu.IsActive = false;

            await _unitOfWork.PlanRepository.Commit();
            return await Task.FromResult(true);
        }
    }
}
