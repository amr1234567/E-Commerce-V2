using ECommerce.Api.Helpers;
using ECommerce.DataAccess.Repositories;
using ECommerce.Services.Helpers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ECommerce.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiLayerServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinaryConfig>(configuration.GetSection("cloudinary"));
            services.AddExceptionHandler<CustomExceptionHandler>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static WebApplication UseApiLayer(this WebApplication app)
        {

            return app;
        }
    }
}
