using AutoMapper;
using SpaManagement.Domain.Entities;
using SpaManagement.DTOs;

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
