using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpaManagement.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
