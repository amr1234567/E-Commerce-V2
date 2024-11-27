using Dapper;
using ECommerce.DataAccess.Constants;
using ECommerce.Domain.Identity;
using ECommerce.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Data;
using System.Linq;
using System.Text.Json;

namespace ECommerce.DataAccess.Repositories
{
    public class WishListRepository
        (EFApplicationContext context, AppDapperContext dapperContext,
        IDistributedCache cache, ILogger<WishListRepository> logger,IProductRepository productRepository)
        : IWishListRepository
    {
        private readonly IDbConnection dbConnection = dapperContext.CreateConnection();

        public async Task<WishList> AddProductsToWishList(int wishListId, params Product[] products)
        {
            var wishList = await context.WishLists
                .Include(w => w.Products)
                .FirstOrDefaultAsync(w => w.Id == wishListId);
            if (wishList == null)
                throw new EntityNotFoundException(typeof(WishList), wishListId);
            foreach (var product in products)
            {
                wishList.Products ??= [];
                wishList.Products.Add(product);
            }
            var loggerInfo = string.Join('-', products.Select(p => p.Id));
            logger.LogDebug($"products with ids : '{loggerInfo}' has been ADDED for wishList with id {wishListId}");
            var cacheId = GetCacheKey(wishListId);
            await cache.SetStringAsync(cacheId, JsonSerializer.Serialize(wishList), CachingConfiguration.CachingConfigurationParameter);
            return wishList;
        }

        public async Task<WishList> AddProductsToWishList(int wishListId, params int[] productIds)
        {
            var wishList = await context.WishLists
                .Include(w => w.Products)
                .FirstOrDefaultAsync(w => w.Id == wishListId);
            if (wishList == null)
                throw new EntityNotFoundException(typeof(WishList), wishListId);
            foreach (var productId in productIds)
            {
                wishList.Products ??= [];
                var product = await productRepository.GetProductById(productId);
                wishList.Products.Add(product);
            }
            var loggerInfo = string.Join('-', productIds);
            logger.LogDebug($"products with ids : '{loggerInfo}' has been ADDED for wishList with id {wishListId}");
            var cacheId = GetCacheKey(wishListId);
            await cache.SetStringAsync(cacheId, JsonSerializer.Serialize(wishList), CachingConfiguration.CachingConfigurationParameter);
            return wishList;
        }

        public async Task<WishList> AddProductsToWishList(int customerId, string wishListName, params Product[] products)
        {
            var wishList = await context.WishLists
               .Include(w => w.Products)
               .FirstOrDefaultAsync(w => w.CustomerId == customerId && w.Name == wishListName);
            if (wishList == null)
                throw new EntityNotFoundException(typeof(WishList), wishListName);
            foreach (var product in products)
            {
                wishList.Products ??= [];
                wishList.Products.Add(product);
            }
            var loggerInfo = string.Join('-', products.Select(p => p.Id));
            logger.LogDebug($"products with ids : '{loggerInfo}' has been ADDED for wishList with id {wishList.Id}");
            var cacheId = GetCacheKey(wishList.Id);
            await cache.SetStringAsync(cacheId, JsonSerializer.Serialize(wishList), CachingConfiguration.CachingConfigurationParameter);
            return wishList;
        }

        public async Task<WishList> AddProductsToWishList(int customerId, string wishListName, params int[] productIds)
        {
            var wishList = await context.WishLists
               .Include(w => w.Products)
               .FirstOrDefaultAsync(w => w.CustomerId == customerId && w.Name == wishListName);
            if (wishList == null)
                throw new EntityNotFoundException(typeof(WishList), wishListName);
            foreach (var productId in productIds)
            {
                wishList.Products ??= [];
                var product = await productRepository.GetProductById(productId);
                wishList.Products.Add(product);
            }
            var loggerInfo = string.Join('-', productIds);
            logger.LogDebug($"products with ids : '{loggerInfo}' has been ADDED for wishList with id {wishList.Id}");
            var cacheId = GetCacheKey(wishList.Id);
            await cache.SetStringAsync(cacheId, JsonSerializer.Serialize(wishList), CachingConfiguration.CachingConfigurationParameter);
            return wishList;
        }

        public async Task<WishList> CreateWishList(WishList wishList)
        {
            ArgumentNullException.ThrowIfNull(wishList, nameof(wishList));
            await context.WishLists.AddAsync(wishList);
            logger.LogDebug($"A new wishList has been CREATED with id {wishList.Id}");
            var cacheId = GetCacheKey(wishList.Id);
            await cache.SetStringAsync(cacheId, JsonSerializer.Serialize(wishList), CachingConfiguration.CachingConfigurationParameter);

            return wishList;
        }

        public async Task DeleteProductsFromWishList(int wishListId, params Product[] products)
        {
            var wishList = await context.WishLists
               .Include(w => w.Products)
               .FirstOrDefaultAsync(w => w.Id == wishListId);
            if (wishList == null)
                throw new EntityNotFoundException(typeof(WishList), wishListId);
            if (wishList.Products == null || !wishList.Products.Any())
            {
                logger.LogDebug($"wish list with id '{wishListId}' is empty or null");
                return;
            }
            foreach (var product in products)
            {
                wishList.Products.Remove(product);
            }
            var loggerInfo = string.Join('-', products.Select(p => p.Id));
            logger.LogDebug($"products with ids : '{loggerInfo}' has been DELETED for wishList with id {wishListId}");
            var cacheId = GetCacheKey(wishListId);
            await cache.SetStringAsync(cacheId, JsonSerializer.Serialize(wishList), CachingConfiguration.CachingConfigurationParameter);
        }

        public async Task DeleteProductsFromWishList(int wishListId, params int[] productIds)
        {
            var wishList = await context.WishLists
               .Include(w => w.Products)
               .FirstOrDefaultAsync(w => w.Id == wishListId);
            if (wishList == null)
                throw new EntityNotFoundException(typeof(WishList), wishListId);
            if (wishList.Products == null || !wishList.Products.Any())
            {
                logger.LogDebug($"wish list with id '{wishListId}' is empty or null");
                return;
            }
            foreach (var productId in productIds)
            {
                var product = await productRepository.GetProductById(productId);
                wishList.Products.Remove(product);
            }

            var loggerInfo = string.Join('-', productIds);
            logger.LogDebug($"products with ids : '{loggerInfo}' has been DELETED for wishList with id {wishListId}");

            var cacheId = GetCacheKey(wishListId);
            await cache.SetStringAsync(cacheId, JsonSerializer.Serialize(wishList), CachingConfiguration.CachingConfigurationParameter);
        }

        public async Task DeleteProductsFromWishList(int customerId, string wishListName, params Product[] products)
        {
            var wishList = await context.WishLists
               .Include(w => w.Products)
               .FirstOrDefaultAsync(w => w.CustomerId == customerId && w.Name == wishListName);
            if (wishList == null)
                throw new EntityNotFoundException(typeof(WishList), wishListName);
            if (wishList.Products == null || !wishList.Products.Any())
            {
                logger.LogDebug($"wish list with id '{wishList.Id}' is empty or null");
                return;
            }
            foreach (var product in products)
            {
                wishList.Products.Remove(product);
            }

            var loggerInfo = string.Join('-', products.Select(p => p.Id));
            logger.LogDebug($"products with ids : '{loggerInfo}' has been DELETED for wishList with id {wishList.Id}");

            var cacheId = GetCacheKey(wishList.Id);
            await cache.SetStringAsync(cacheId, JsonSerializer.Serialize(wishList), CachingConfiguration.CachingConfigurationParameter);
        }

        public async Task DeleteProductsFromWishList(int customerId, string wishListName, params int[] productIds)
        {
            var wishList = await context.WishLists
              .Include(w => w.Products)
              .FirstOrDefaultAsync(w => w.CustomerId == customerId && w.Name == wishListName);
            if (wishList == null)
                throw new EntityNotFoundException(typeof(WishList), wishListName);
            if (wishList.Products == null || !wishList.Products.Any())
            {
                logger.LogDebug($"wish list with id '{wishList.Id}' is empty or null");
                return;
            }
            foreach (var productId in productIds)
            {
                var product = await productRepository.GetProductById(productId);
                wishList.Products.Remove(product);
            }

            var loggerInfo = string.Join('-', productIds);
            logger.LogDebug($"products with ids : '{loggerInfo}' has been DELETED for wishList with id {wishList.Id}");

            var cacheId = GetCacheKey(wishList.Id);
            await cache.SetStringAsync(cacheId, JsonSerializer.Serialize(wishList), CachingConfiguration.CachingConfigurationParameter);

        }

        public async Task<int> DeleteWishList(int id)
        {
            await cache.RemoveAsync(GetCacheKey(id));
            await context.WishLists.Where(w=>w.Id == id).ExecuteDeleteAsync();
            return id;
        }

        public async Task<List<WishList>> GetAllForCustomer(int customerId)
        {
            var sqlQuery = "Select * from WishList where CustomerId = @customerId order by CreatedDate Desc";
            var parameters = new { customerId };
            var wishLists = await dbConnection.QueryAsync<WishList>(sqlQuery, parameters);
            return wishLists.ToList();
        }

        public async Task<WishList> GetWishList(int id)
        {
            var sqlQuery = "Select * from WishList where Id = @id;";
            var parameters = new { id };
            var wishLists = await dbConnection.QueryAsync<WishList>(sqlQuery, parameters);
            var wishList = wishLists.SingleOrDefault();
            return wishList ?? throw new EntityNotFoundException(typeof(WishList), id);
        }

        public async Task<WishList> UpdateWishList(WishList wishList)
        {
            await context.WishLists.Where(w => w.Id == wishList.Id).ExecuteUpdateAsync(wl =>
            wl
            .SetProperty(w => w.Name, wishList.Name)
            .SetProperty(w => w.Description, wishList.Description)
            );
            return wishList;
        }

        private string GetCacheKey(object wishListId) => "WishList_" + wishListId;

    }
}
