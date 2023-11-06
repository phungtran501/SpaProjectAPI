using Microsoft.AspNetCore.Authentication.JwtBearer;
using SpaManagement.Domain.Entities;
using SpaManagement.DTOs;

namespace SpaManagement.Authentication.Service
{
    public interface ITokenHandler
    {
        Task<(string, DateTime)> CreateAccessToken(ApplicationUser customer);
        Task<(string, string, DateTime)> CreateRefreshToken(ApplicationUser customer);
        Task<JwtModel> ValidateRefreshToken(string refreshToken);
        Task ValidateToken(TokenValidatedContext context);
    }
}