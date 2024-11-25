using ECommerce.DataAccess.Constants;
using ECommerce.DataAccess.EFContext;
using ECommerce.DataAccess.Exceptions;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Identity;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repositories
{
    public class BasketRepository
        (EFApplicationContext context,IProductRepository productRepository, IDistributedCache redisCache)
        : IBasketRepository
    {
        public async Task<int> AddProduct(int basketId, Product product)
        {
            try
            {
                var basket = await GetBasketById(basketId);
                await AddingOrderItemFunctionality(basket, product);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> AddProduct(int basketId, int productId)
        {
            try
            {
                var product = await productRepository.GetProductById(productId);
                return await AddProduct(basketId, productId);
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> AddProductRange(int basketId, params Product[] products)
        {
            try
            {
                var basket = await GetBasketById(basketId);
                foreach (var product in products)
                {
                    await AddingOrderItemFunctionality(basket, product);
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> AddProductRange(int basketId, params int[] productIds)
        {
            try
            {
                var basket = await GetBasketById(basketId);
                foreach (var productId in productIds)
                {
                    var product = await productRepository.GetProductById(productId);
                    await AddingOrderItemFunctionality(basket, product);
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> CreateNewBasket(Basket basket)
        {
            ArgumentNullException.ThrowIfNull(basket, nameof(basket));
            await context.Baskets.AddAsync(basket);
            await UpdateBasketCache(basket);
            return basket.Id;
        }

        public async Task<int> DeleteBasketByCustomerId(int customerId)
        {
            var basket = await context.Baskets.FirstOrDefaultAsync(b => b.CustomerId == customerId)
                ?? throw new EntityNotFoundException(typeof(Basket), customerId);

            var cacheKey = GetBasketCacheKey(basket.Id);
            await redisCache.RemoveAsync(cacheKey);

            return await context.Baskets.Where(b=>b.CustomerId == customerId).ExecuteDeleteAsync();
        }

        public async Task<int> DeleteBasketById(int basketId)
        {
            //--- Cashing --- 
            var cacheKey = GetBasketCacheKey(basketId);
            await redisCache.RemoveAsync(cacheKey);
            return await context.Baskets.Where(b => b.Id == basketId).ExecuteDeleteAsync();
        }

        public async Task<Basket> GetBasketByCustomerId(int customerId)
        {
            var basket = await context.Baskets.AsNoTracking().FirstOrDefaultAsync(b => b.CustomerId == customerId)
                    ?? throw new EntityNotFoundException(typeof(Basket).Name, customerId);

            var orderItems = await context.OrderItems.AsNoTracking().Where(o => o.BasketId == basket.Id).ToListAsync();
            basket.OrderItems = orderItems;
            return basket;
        }

        public async Task<Basket> GetBasketById(int basketId)
        {
            var cacheKey = GetBasketCacheKey(basketId);
            var cachedBasket = await redisCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<Basket>(cachedBasket);
            }

            var basket = await context.Baskets.AsNoTracking()
                 .Include(b => b.OrderItems.Where(o => o.BasketId == basketId))
                 .FirstOrDefaultAsync(b => b.Id == basketId)
                 ?? throw new EntityNotFoundException(typeof(Basket).Name, basketId);

            await redisCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(basket),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
                });

            return basket;
        }

        public async Task<int> RemoveProduct(int basketId, int productId)
        {
            var basket = await GetBasketById(basketId);
            await RemovingOrderItemFunctionality(basket, productId);
            return 1;
        }

        public async Task<int> RemoveProductRange(int basketId, params int[] productIds)
        {
            var basket = await GetBasketById(basketId);
            foreach (var productId in productIds)
                await RemovingOrderItemFunctionality(basket, productId);
            return 1;
        }
        
        
        private async Task AddingOrderItemFunctionality(Basket basket, Product product)
        {
            if (basket.OrderItems is not null && basket.OrderItems.Any(o => o.ProductId == product.Id))
            {
                var orderItem = await context.OrderItems.FirstOrDefaultAsync(o => o.BasketId == basket.Id && o.ProductId == product.Id);
                if (orderItem != null)
                {
                    orderItem.Quantity++;
                    foreach(var orderItemInOrderItems in basket.OrderItems)
                    {
                        if(orderItemInOrderItems.Id == orderItem.Id)
                        {
                            orderItemInOrderItems.Quantity++;
                        }
                    }
                    context.OrderItems.Update(orderItem);
                }
                else
                {
                    throw new EntityExistsException(typeName: typeof(Product).Name, basket.Id);
                }
            }
            else
            {
                var newOrderItem = new OrderItem
                {
                    BasketId = basket.Id,
                    ProductId = product.Id,
                    Quantity = 1,
                    Price = product.Price,
                };
                await context.OrderItems.AddAsync(newOrderItem);
                basket.OrderItems = [..basket.OrderItems ?? [] , newOrderItem];

            }
            await UpdateBasketCache(basket);
        }

        private async Task RemovingOrderItemFunctionality(Basket basket, int productId)
        {
            if (basket.OrderItems is not null && basket.OrderItems.Any(o => o.ProductId == productId))
            {
                var orderItem = await context.OrderItems.FirstOrDefaultAsync(o => o.BasketId == basket.Id && o.ProductId == productId);
                if (orderItem != null)
                {
                    if (orderItem.Quantity > 0)
                    {
                        orderItem.Quantity--;
                        foreach (var orderItemInOrderItems in basket.OrderItems)
                        {
                            if (orderItemInOrderItems.Id == orderItem.Id)
                            {
                                orderItemInOrderItems.Quantity--;
                            }
                        }
                    }
                    else
                    {
                        context.OrderItems.Remove(orderItem);
                    }
                    await UpdateBasketCache(basket);
                }
                else
                {
                    throw new EntityNotFoundException(typeof(Product).Name, basket.Id);
                }
            }
            else
            {
                throw new EntityNotFoundException(typeof(Product).Name, productId);
            }
        }

        private async Task UpdateBasketCache(Basket basket)
        {
            var cacheKey = GetBasketCacheKey(basket.Id);

            await redisCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(basket),
                CachingConfiguration.CachingConfigurationParameter);
        }

        private string GetBasketCacheKey(int id)
        {
            return "Basket_" + id.ToString();
        }
    }
}
