using ECommerce.Services.Abstractions;
using ECommerce.Services.ExternalServices;
using ECommerce.Services.Helpers;
using ECommerce.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using ECommerce.Services.Models.Outputs;
using ECommerce.Services.Behaviors;

namespace ECommerce.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesLayerDI(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<AccountServicesHelpers>();
            services.AddScoped<IStorageServices, StorageServices>();
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(TokenModel).Assembly);
                //config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            var jwtConfig = new JwtHelper();
            configuration.GetSection("Jwt").Bind(jwtConfig);

            services.AddAuthServices(jwtConfig);

            return services;
        }

        private static IServiceCollection AddAuthServices(this IServiceCollection services, JwtHelper jwtConfig)
        {
            
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtConfig.JwtIssuer,
                        ValidAudience = jwtConfig.JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.JwtKey)),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            return services;
        }


    }
}
