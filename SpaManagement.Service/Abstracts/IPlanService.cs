using SpaManagement.Domain.Entities;
using SpaManagement.Service.DTOs;

namespace SpaManagement.Service.Abstracts
{
    public interface IPlanService
    {
        Task<IEnumerable<PlanDTO>> GetListPlan(int pageIndex, int pageSize);
        Task DeletePlan(int key);
        Task<PlanDTO> GetPlanById(int id);
        Task<ResponseModel> CreateUpdate(PlanDTO planDTO);
    }
}