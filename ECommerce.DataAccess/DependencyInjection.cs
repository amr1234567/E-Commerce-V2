using ECommerce.DataAccess.DapperContext;
using ECommerce.DataAccess.EFContext;
using ECommerce.DataAccess.Repositories;
using ECommerce.Domain.Abstractions;
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

            return services;
        }
    }
}
