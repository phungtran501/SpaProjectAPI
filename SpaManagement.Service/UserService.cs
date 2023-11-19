using Microsoft.AspNetCore.Identity;
using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Service.Abstracts;

namespace SpaManagement.Service
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        UserManager<ApplicationUser> _userManager;
        PasswordHasher<ApplicationUser> _passwordHasher;
        public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, PasswordHasher<ApplicationUser> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _passwordHasher = passwordHasher;

        }

        public async Task<ApplicationUser> CheckLogin(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if(user == null)
            {
                return default(ApplicationUser);
            }

            var isExist = await _userManager.CheckPasswordAsync(user, password);

            if(!isExist)
            {
                return default(ApplicationUser);
            }
            return user;
        }

    }
}
