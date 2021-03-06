using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PolicyApp.Auth.Requirements;
using PolicyApp.Core.Repositories;
using PolicyApp.Core.Repositories.Interfaces;
using PolicyApp.Core.Requirements.Handlers;
using PolicyApp.Core.Services;
using PolicyApp.Core.Services.Interfaces;
using System.Text;

namespace PolicyApp.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, ShouldBeAMetalFanAuthHandler>();
            services.AddSingleton<ITokenManager, TokenManager>();
            //repositories
            services.AddSingleton<IUserRepository, UserRepository>();
            //services
            services.AddSingleton<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services)
        {
            var builder = services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            builder.AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(TokenConstants.Key)),
                    ValidIssuer = TokenConstants.Issuer,
                    ValidAudience = TokenConstants.Audience
                };
            });

            return services;
        }

        public static IServiceCollection AddPolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(
                config =>
                {
                    config.AddPolicy("ShouldBeAMetalFan", options =>
                    {
                        options.RequireAuthenticatedUser();
                        options.AuthenticationSchemes.Add("Beer");
                        options.Requirements.Add(new ShouldBeAMetalFanRequirement());
                    });
                });

            return services;
        }
    }
}
