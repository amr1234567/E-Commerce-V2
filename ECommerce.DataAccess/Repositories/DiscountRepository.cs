using ECommerce.DataAccess.Constants;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ECommerce.DataAccess.Repositories
{
    public class DiscountRepository
        (EFApplicationContext context, IDistributedCache cache,ILogger<DiscountRepository> logger)
        : IDiscountRepository
    {
        private string GetCacheKey(object discountId) => "Discount_" + discountId;

        public async Task<int> CreateDiscount(Discount discount)
        {
            ArgumentNullException.ThrowIfNull(discount, nameof(discount));
            await context.Discounts.AddAsync(discount);
            await cache.SetStringAsync(GetCacheKey(discount.Id), JsonSerializer.Serialize(discount), CachingConfiguration.CachingConfigurationParameter);
            logger.LogInformation($"Discount with Id '{discount.Id}' CREATED successfully");
            return discount.Id;
        }

        public async Task<int> DeleteDiscount(int discountId)
        {
            await context.Discounts.Where(d => d.Id == discountId).ExecuteDeleteAsync();
            await cache.RemoveAsync(GetCacheKey(discountId));
            logger.LogInformation($"Discount with Id '{discountId}' DELETED successfully");
            return discountId;
        }

        public async Task<Discount> GetById(int id)
        {
            var discountFromCache = await cache.GetStringAsync(GetCacheKey(id));
            if (!string.IsNullOrEmpty(discountFromCache))
            {
                return JsonSerializer.Deserialize<Discount>(discountFromCache);
            }

            var discountFromDb = await context.Discounts.AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id)
                ?? throw new EntityNotFoundException(typeof(Discount), id);
           
            await cache.SetStringAsync(GetCacheKey(discountFromDb.Id), JsonSerializer.Serialize(discountFromDb), CachingConfiguration.CachingConfigurationParameter);
            
            return discountFromDb;

        }

        public async Task<int> UpdateDiscount(Discount discount)
        {
            var discountFromDb = await context.Discounts
                .Where(d => d.Id == discount.Id).ExecuteUpdateAsync(u =>
                    u.SetProperty(d => d.MinPriceToApply, discount.MinPriceToApply)
                        .SetProperty(d => d.DiscountAmount, discount.DiscountAmount)
                        .SetProperty(d => d.EndAt, discount.EndAt)
                );

            await cache.SetStringAsync(GetCacheKey(discount.Id), JsonSerializer.Serialize(discountFromDb), CachingConfiguration.CachingConfigurationParameter);
            return discount.Id;
        }

        public async Task<DiscountLog> UseDiscount(int discountId,int customerId)
        {
            var newDiscountLog = new DiscountLog
            {
                CustomerId = customerId,
                DiscountId = discountId,
            };
            await context.DiscountLogs.AddAsync(newDiscountLog);

            logger.LogInformation($"Discount with Id '{discountId}' USED BY customer with id '{customerId}'");

            return newDiscountLog;
        }
    }
}
