using Dapper;
using Microsoft.AspNetCore.Identity;
using SpaManagement.Data;
using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Domain.Enums;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDapperHelper _dapperHelper;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IDapperHelper dapperHelper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dapperHelper = dapperHelper;
        }

        public async Task<IEnumerable<AccountDTO>> GetAllListAccount(int pageIndex, int pageSize)
        {

            IQueryable<ApplicationUser> users = _userManager.Users.Where(x => x.IsActive && x.UserName != "Administrator");  //get all uses in db

            DynamicParameters dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("pageIndex", pageIndex, DbType.Int32, ParameterDirection.Input);
            dynamicParameters.Add("pageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            dynamicParameters.Add("totalRecord", 0, DbType.Int32, ParameterDirection.Output);

            var result = await _dapperHelper.ExcuteStoreProcedureReturnList<AccountDTO>("GetAllUsers", dynamicParameters);

            var total = dynamicParameters.Get<int>("totalRecord");


            var data = result.Select(x => new AccountDTO
            {
                Id = x.Id,
                Username = x.Username,
                RoleName = x.RoleName,
                Fullname = x.Fullname,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                IsSystem = x.IsSystem,
                IsActive = x.IsActive
            }).ToArray();


            return data;
        }

       public async Task<AccountDTO> GetAccountById(string userId)
        {
            var account = await _userManager.FindByIdAsync(userId);

            var roles = await _userManager.GetRolesAsync(account);

            var user = new AccountDTO
            {
                Id = account.Id,
                Address = account.Address,
                Username = account.UserName,
                Email = account.Email,
                Fullname = account.Fullname,
                PhoneNumber = account.PhoneNumber,
                IsSystem = account.IsSystem,
                IsActive = account.IsActive,
                RoleName = roles.FirstOrDefault()
            };

            return user;
        }

        public async Task<ResponseModel> CreateUpdate(AccountDTO accountDTO)
        {
            if (string.IsNullOrEmpty(accountDTO.Id))
            {
                var user = new ApplicationUser
                {
                    UserName = accountDTO.Username,
                    Email = accountDTO.Email,
                    PhoneNumber = accountDTO.PhoneNumber,
                    Fullname = accountDTO.Fullname,
                    Address = accountDTO.Address,
                    IsActive = accountDTO.IsActive,
                    IsSystem = accountDTO.IsSystem,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = true,
                    LockoutEnabled = true,
                    AccessFailedCount = 0

                };

                var result = await _userManager.CreateAsync(user, accountDTO.Password);

                if (result.Succeeded)
                {
                    var roles = await _userManager.AddToRoleAsync(user, accountDTO.RoleName);

                    return new ResponseModel
                    {
                        Status = true,
                        Message = "Insert successful",
                        StatusType = StatusType.Success,
                        Action = ActionType.Insert
                    };
                }
                else
                {
                    var errors = result.Errors.ToList().Select(x => x.Description);

                    return new ResponseModel
                    {
                        Status = false,
                        Message = string.Join(';', errors),
                        StatusType = StatusType.Fail,
                        Action = ActionType.Insert
                    };
                }
            }
            //update
            else
            {
                var user = await _userManager.FindByIdAsync(accountDTO.Id);

                var roles = await _userManager.GetRolesAsync(user);


                var isExist = await _userManager.IsInRoleAsync(user, accountDTO.RoleName);

                if (!isExist)
                {
                    await _userManager.RemoveFromRoleAsync(user, accountDTO.RoleName);
                    await _userManager.AddToRoleAsync(user, accountDTO.RoleName);
                }


                user.Address = accountDTO.Address;
                user.Fullname = accountDTO.Fullname;
                user.Email = accountDTO.Email;
                user.Address = accountDTO.Address;
                user.IsActive = accountDTO.IsActive;
                user.IsSystem = accountDTO.IsSystem;
                user.PhoneNumber = accountDTO.PhoneNumber;


                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return new ResponseModel
                    {
                        Status = true,
                        Message = "Update successful",
                        StatusType = StatusType.Success,
                        Action = ActionType.Update
                    };
                }
                else
                {
                    return new ResponseModel
                    {
                        Status = false,
                        Message = "Update failed",
                        StatusType = StatusType.Fail,
                        Action = ActionType.Update
                    };
                }
            }
        }

        public async Task DeleteAccount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.IsActive = false;
            await _userManager.UpdateAsync(user);
        }

        public async Task<IEnumerable<AccountDTO>> GetUsers()
        {
            IQueryable<ApplicationUser> users = _userManager.Users.Where(x => x.IsActive && x.UserName != "Administrator" && !x.IsSystem);

            var result = users.Select(x => new AccountDTO
            {
                Id = x.Id,
                Username = x.UserName,
                
            });

            return result;
        }

    }
}
