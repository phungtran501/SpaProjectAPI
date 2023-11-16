using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpaManagement.Domain.Entities;
using SpaManagement.DTOs;
using SpaManagement.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SpaManagement.Authentication.Service
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration _configuration;
        IUserService _customerService;
        IUserTokenService _userTokenService;
        UserManager<ApplicationUser> _userManager;

        public TokenHandler(IConfiguration configuration, IUserService customerService, IUserTokenService userTokenService ,UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _customerService = customerService;
            _userTokenService = userTokenService;
            _userManager = userManager;
        }

        public async Task<(string, DateTime)> CreateAccessToken(ApplicationUser customer)
        {
            DateTime expiredDateAccess = DateTime.Now.AddSeconds(5);

            var roles = await _userManager.GetRolesAsync(customer);

            var cliams = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["TokenBear:Issuer"], ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Aud, "RestaurantManagement", ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Exp, expiredDateAccess.ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String, _configuration["TokenBear: Issuer"]),
                new Claim(ClaimTypes.Name, customer.Fullname, ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim("Username", customer.UserName, ClaimValueTypes.String, _configuration["TokenBear:Issuer"])
            }.Union(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"]));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenInfo = new JwtSecurityToken(
                issuer: _configuration["TokenBear:Issuer"],
                audience: _configuration["TokenBear:Issuer"],
                claims: cliams,
                notBefore: DateTime.Now,
                expires: expiredDateAccess,
                credential
                );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(tokenInfo);

            return await Task.FromResult((accessToken, expiredDateAccess));
        }

        public async Task<(string, string, DateTime)> CreateRefreshToken(ApplicationUser customer)
        {
            DateTime expiredDateRefresh = DateTime.Now.AddHours(3);
            string code = Guid.NewGuid().ToString();
            var cliams = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["TokenBear:Issuer"], ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, _configuration["TokenBear:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Exp, expiredDateRefresh.ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
                new Claim(ClaimTypes.SerialNumber, code, ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"]));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenInfo = new JwtSecurityToken(
                issuer: _configuration["TokenBear:Issuer"],
                audience: _configuration["TokenBear:Issuer"],
                claims: cliams,
                notBefore: DateTime.Now,
                expires: expiredDateRefresh,
                credential
                );

            string refreshToken = new JwtSecurityTokenHandler().WriteToken(tokenInfo);

            return await Task.FromResult((code, refreshToken, expiredDateRefresh));
        }

        public async Task ValidateToken(TokenValidatedContext context)
        {
            var claims = context.Principal.Claims.ToList();
            if (claims.Count == 0)
            {
                context.Fail("This token contains no information");
                return;
            }

            var identity = context.Principal.Identity as ClaimsIdentity;
            if (identity.FindFirst("Username") == null)
            {
                string username = identity.FindFirst("Username").Value;
                var user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    context.Fail("This token is invalid for user");
                    return;
                }
            }
        }

        public async Task<JwtModel> ValidateRefreshToken(string refreshToken)
        {
            JwtModel jwtModel = new();
            var cliamPriciple = new JwtSecurityTokenHandler().ValidateToken(refreshToken, new TokenValidationParameters
            {
                RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"])),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            },
            out _

            );
            if (cliamPriciple == null) return new();

            string code = cliamPriciple.Claims.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber)?.Value;

            if (string.IsNullOrEmpty(code)) return new();

            UserToken userToken = await _userTokenService.CheckRefreshToken(code);

            //if (userToken != null)
            //{
            //    Customer customer= await _customerService.FindById(userToken.Id);
            //    (string newAccessToken, DateTime createdDate) = await CreateAccessToken(customer);
            //    (string codeRefreshToken, string newRefreshToken, DateTime newDateCreated) = await CreateRefreshToken(customer);

            //    return new JwtModel
            //    {
            //        AccessToken = newAccessToken,
            //        RefreshToken = newRefreshToken,
            //        Fullname = customer.FullName,
            //        Username = customer.UserName,
            //    };
            //}

            return new();
        }
    }
}
