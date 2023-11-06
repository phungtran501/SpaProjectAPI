using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SpaManagement.Domain.Entities;
using SpaManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Infrastructure.Configuration
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserModel, ApplicationUser>()
                .ForMember(dest => dest.PasswordHash, y => y.MapFrom(scr => scr.Password))
                .ReverseMap();
        }
    }


}
