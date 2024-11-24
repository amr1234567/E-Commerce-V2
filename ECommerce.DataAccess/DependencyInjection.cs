using ECommerce.DataAccess.DapperContext;
using ECommerce.DataAccess.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<EFApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Def"));
            });

            services.AddSingleton<AppDapperContext>();

            return services;
        }
    }
}
