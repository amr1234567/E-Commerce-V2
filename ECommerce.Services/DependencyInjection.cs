using ECommerce.Services.ExternalServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesLayerDI(this IServiceCollection services)
        {

            services.AddScoped<TokenServices>();
            services.AddScoped<RegistrationHelperServices>();
            services.AddScoped<ImageServices>();
            return services;
        }
    }
}
