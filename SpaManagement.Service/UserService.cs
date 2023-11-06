using Microsoft.AspNetCore.Identity;
using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //var customer = await _unitOfWork.CustomerRepository.GetSingleByConditionAsync(x => x.UserName == username && x.Password == password);
            //return customer;
            return user;
        }

        public async Task<Customer> FindByUsername(string username)
        {
            return await _unitOfWork.CustomerRepository.GetSingleByConditionAsync(x => x.UserName == username);
        }

        public async Task<Customer> FindById(int customerId)
        {
            return await _unitOfWork.CustomerRepository.GetSingleByConditionAsync(x => x.Id == customerId);
        }
    }
}
