using ECommerce.DataAccess.DapperContext;
using ECommerce.DataAccess.EFContext;
using ECommerce.DataAccess.Repositories;
using ECommerce.Domain.Abstractions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("redis");
            });

            services.AddSingleton<AppDapperContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWishListRepository, WishListRepository>();


            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("Def")!)
                .AddRedis(configuration.GetConnectionString("redis")!);

            return services;
        }

        public static WebApplication UseDataAccess(this WebApplication application)
        {

            application.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return application;
        }
    }
}
