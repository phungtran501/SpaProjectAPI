using SpaManagement.Domain.Entities;

namespace SpaManagement.Service
{
    public interface IUserService
    {
        Task<ApplicationUser> CheckLogin(string username, string password);
        Task<Customer> FindById(int customerId);
        Task<Customer> FindByUsername(string username);
    }
}