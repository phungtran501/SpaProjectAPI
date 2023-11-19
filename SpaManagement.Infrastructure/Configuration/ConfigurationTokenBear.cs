using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SpaManagement.Authentication.Service;

namespace SpaManagement.Infrastructure.Configuration
{
    public static class ConfigurationTokenBear
    {
        public static void RegisterTokenBear(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["TokenBear:Issuer"], //check issuer
                    ValidateIssuer = false, //not check issuer
                    ValidAudience = configuration["TokenBear:Audience"],
                    ValidateAudience = false, //not check Audience
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenBear:SignatureKey"])), //tail of token
                    ValidateIssuerSigningKey = true, //accept to check the tail of token
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents() //where to do the checking token
                {
                    OnTokenValidated = context =>
                    {
                        var tokenHandler = context.HttpContext.RequestServices.GetRequiredService<ITokenHandler>();

                        return tokenHandler.ValidateToken(context);
                    },
                    OnAuthenticationFailed = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
