using SpaManagement.Domain.Entities;

namespace SpaManagement.Service
{
    public interface IPlanService
    {
        Task<IEnumerable<Plan>> GetPlan();
        Task<bool> UpdatePlan(int id);
    }
}