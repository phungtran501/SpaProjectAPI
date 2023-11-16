using SpaManagement.Domain.Entities;

namespace SpaManagement.Service.Abstracts
{
    public interface IUserService
    {
        Task<ApplicationUser> CheckLogin(string username, string password);

    }
}