using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpaManagement.Domain.Entities;
using SpaManagement.Domain.Enums;
using SpaManagement.Service.Abstracts;
using SpaManagement.Service.DTOs;
using SpaManagement.Service.DTOs.Product;

namespace SpaManagement.Service
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<string>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var result = roles.Select(x => x.Name);

            return result;
        }

        public async Task<IEnumerable<RoleDTO>> GetListRole(int pageIndex, int pageSize)
        {
            var roles = _roleManager.Roles;

            var result = roles.Skip((pageIndex - 1) * pageSize).Take(pageIndex * pageSize).ToList();

            int total = roles.Count();

            var data = result.Select(x => new RoleDTO
            {
                Id = x.Id,
                Name = x.Name,
            });

            return data;
        }

        public async Task<ResponseModel> CreateUpdate(RoleDTO roleDTO)
        {
            if (string.IsNullOrEmpty(roleDTO.Id))
            {
                var role = new IdentityRole
                {
                    Name = roleDTO.Name,
                };

                var result = await _roleManager.CreateAsync(role);

                return result.Succeeded
                    ? new ResponseModel { Status = true, Message = "Insert successful", StatusType = StatusType.Success }
                    : new ResponseModel { Status = false, Message = string.Join(';', result.Errors.Select(x => x.Description)), StatusType = StatusType.Fail };
            }
            else
            {
                var role = await _roleManager.FindByIdAsync(roleDTO.Id);
                if (role == null)
                {
                    return new ResponseModel { Status = false, Message = "Role not found", StatusType = StatusType.Fail };
                }

                role.Name = roleDTO.Name;

                var result = await _roleManager.UpdateAsync(role);

                return result.Succeeded
                    ? new ResponseModel { Status = true, Message = "Update successful", StatusType = StatusType.Success }
                    : new ResponseModel { Status = false, Message = result.Errors.FirstOrDefault()?.Description, StatusType = StatusType.Fail };
            }

        }

        public async Task DeleteRole(string key)
        {
            var role = await _roleManager.FindByIdAsync(key);

            await _roleManager.DeleteAsync(role);
        }

        public async Task<RoleDTO> GetRoleById(string id)
        {

            var role = await _roleManager.FindByIdAsync(id);

            var result = new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
            };


            return result;
        }
    }
}
