using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpaManagement.Authentication.Service;
using SpaManagement.Data;
using SpaManagement.Data.Abstract;
using SpaManagement.Domain.Entities;
using SpaManagement.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Infrastructure.Configuration
{
    public static class ConfigurationServer
    {
        public static void RegisterContextDb(this IServiceCollection service, IConfiguration configaration)
        {
            service.AddDbContext<SpaManagementContext>(options => options
            .UseSqlServer(configaration.GetConnectionString("SpaDB"),
                                options => options.MigrationsAssembly(typeof(SpaManagementContext).Assembly.FullName)));


            service.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequireDigit = true;
                config.Password.RequiredLength = 5;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<SpaManagementContext>();
        }

        public static void RegisterDI(this IServiceCollection service)
        {
            service.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            service.AddTransient<IUnitOfWork, UnitOfWork>(); 
            service.AddTransient<IDapperHelper, DapperHelper>();
            service.AddTransient<IPlanService, PlanService>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<ITokenHandler, TokenHandler>();
            service.AddTransient<IUserTokenService, UserTokenService>();
            service.AddTransient<PasswordHasher<ApplicationUser>>();
            service.AddTransient<PasswordValidator<ApplicationUser>>();
            service.AddTransient<IServicesService, ServicesService>();
            

        }
    }
}
