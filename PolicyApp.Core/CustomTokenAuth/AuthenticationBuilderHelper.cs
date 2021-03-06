using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PolicyApp.Auth.Requirements;
using PolicyApp.Core.Repositories;
using PolicyApp.Core.Repositories.Interfaces;
using PolicyApp.Core.Requirements.Handlers;
using PolicyApp.Core.Services;
using PolicyApp.Core.Services.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace PolicyApp.Core.CustomTokenAuth
{
    public static class AuthenticationBuilderHelper
    {
        public static void AddTokenAuthenticationScheme(
            this AuthenticationBuilder builder,
            [NotNull] string scheme,
            [NotNull] TokenAuthenticationConfiguration config)
        {
            if (config == null)
                throw new ArgumentException($"{nameof(config)} parameter must not be null.");

            if (config.TokenLength <= 0)
                throw new ArgumentException($"{nameof(config.TokenLength)} must be greater than zero.");

            if (config.Realm == null)
                throw new ArgumentException($"{nameof(config.Realm)} Property must not be null.");

            builder.AddScheme<AdyOtakoAuthOptions, AdyOtakoTokenAuthHandler>(scheme, options =>
            {
                options.Scheme = scheme;
                options.Realm = config.Realm;
                options.AuthenticationType = config.AuthenticationType;
                options.TokenLength = config.TokenLength;
            });
        }

        /// <summary>
        ///     Add custom token authentication scheme.
        /// </summary>
        /// <param name="builder">The authentication builder</param>
        /// <param name="scheme">The scheme of custom token</param>
        /// <param name="config">The configuration props for registering custom scheme.</param>
        /// <typeparam name="TAuthServiceImplementation">ITokenAuthService implementation type.</typeparam>
        public static void AddTokenAuthenticationScheme<TAuthServiceImplementation>(
            this AuthenticationBuilder builder,
            [NotNull] string scheme,
            [NotNull] TokenAuthenticationConfiguration config)
              where TAuthServiceImplementation : class, IAdyOTakoTokenAuthService
        {
            builder.Services.AddScoped<IAdyOTakoTokenAuthService, TAuthServiceImplementation>();
            AddTokenAuthenticationScheme(builder, scheme, config);
        }

        /// <summary>
        ///     Add custom token authentication scheme.
        /// </summary>
        /// <param name="builder">The authentication builder</param>
        /// <param name="scheme">The scheme of custom token</param>
        /// <param name="config">The configuration props for registering custom scheme.</param>
        /// <param name="serviceLifetime">Service Lifetime of TAuthServiceImplementation</param>
        /// <typeparam name="TAuthServiceImplementation">ITokenAuthService implementation type.</typeparam>
        public static void AddTokenAuthenticationScheme<TAuthServiceImplementation>(
            this AuthenticationBuilder builder,
            [NotNull] string scheme,
            [NotNull] TokenAuthenticationConfiguration config,
            ServiceLifetime serviceLifetime)
              where TAuthServiceImplementation : class, IAdyOTakoTokenAuthService
        {
            builder.Services.Add(new ServiceDescriptor(typeof(IAdyOTakoTokenAuthService), typeof(TAuthServiceImplementation), serviceLifetime));
            AddTokenAuthenticationScheme(builder, scheme, config);
        }

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
