using SpaManagement.Domain.Entities;

namespace SpaManagement.Service.Abstracts
{
    public interface IPlanService
    {
        Task<IEnumerable<Plan>> GetPlan();
        Task<bool> UpdatePlan(int id);
    }
}