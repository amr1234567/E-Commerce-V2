using Dapper;
using ECommerce.DataAccess.Constants;
using ECommerce.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Data;
using System.Data.Common;
using System.Text.Json;

namespace ECommerce.DataAccess.Repositories
{
    public class ProviderRepository
        (EFApplicationContext context,AppDapperContext dapperContext,IDistributedCache cache, ILogger<ProductRepository> logger)
        : IProviderRepository
    {
        private string GetCacheKey(object providerId) => "Provider_" + providerId;
        private readonly IDbConnection dbConnection = dapperContext.CreateConnection();

        public async Task<Provider> CreateProvider(Provider provider)
        {
            ArgumentNullException.ThrowIfNull(provider, nameof(provider));
            await context.Providers.AddAsync(provider);
            await cache.SetStringAsync(GetCacheKey(provider.Id), JsonSerializer.Serialize(provider), CachingConfiguration.CachingConfigurationParameter);
            return provider;
        }

        public async Task<int> DeleteProvider(int providerId)
        {
            await context.Providers.Where(p => p.Id == providerId).ExecuteDeleteAsync();
            await cache.RemoveAsync(GetCacheKey(providerId));
            return providerId;
        }

        public async Task<List<Provider>> GetAll(int page = 1, int size = 10)
        {
            string cacheKey = GetCacheKey($"_page_{page}_size_{size}");

            var cachedProviders = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedProviders))
            {
                return JsonSerializer.Deserialize<List<Provider>>(cachedProviders);
            }

            // ---- DB ----
            var sqlQuery = "Select * from Providers Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";
            var parameters = new { skip = (page - 1) * size, size };
            var categories = await dbConnection.QueryAsync<Provider>(sqlQuery, parameters);

            //--- Cashing --- 
            var serializedData = JsonSerializer.Serialize(categories.ToList());
            await cache.SetStringAsync(cacheKey, serializedData, CachingConfiguration.CachingConfigurationParameter);

            return categories.ToList();
        }

        public async Task<List<Provider>> GetAllInCategory(int categoryId, int page = 1, int size = 10)
        {
            string cacheKey = GetCacheKey($"_categoryId_{categoryId}_page_{page}_size_{size}");

            // --- Check Cache ---
            var cachedProviders = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedProviders))
            {
                return JsonSerializer.Deserialize<List<Provider>>(cachedProviders);
            }

            // --- Fetch from DB ---
            var sqlQuery = "SELECT * FROM Providers WHERE CategoryId = @categoryId ORDER BY Rate OFFSET @skip ROWS FETCH NEXT @size ROWS ONLY";
            var parameters = new { categoryId, skip = (page - 1) * size, size };

            var providers = await dbConnection.QueryAsync<Provider>(sqlQuery, parameters);

            // --- Cache Results ---
            var serializedData = JsonSerializer.Serialize(providers.ToList());
            await cache.SetStringAsync(cacheKey, serializedData, CachingConfiguration.CachingConfigurationParameter);

            return providers.ToList();
        }

        public async Task<Provider> GetById(int id)
        {
            var provider = await context.Providers.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (provider == null)
                throw new EntityNotFoundException(typeof(Provider), id);
            return provider;
        }

        public async Task<Provider> UpdateProvider(Provider provider)
        {
            await context.Providers.Where(p => p.Id == provider.Id)
                .ExecuteUpdateAsync(
                prov => prov.SetProperty(p => p.Address, provider.Address)
                .SetProperty(p => p.Name, provider.Name)
                .SetProperty(p => p.ContactEmail, provider.ContactEmail)
                .SetProperty(p => p.ContactPhoneNumber, provider.ContactPhoneNumber)
                .SetProperty(p => p.Description, provider.Description)
                .SetProperty(p => p.RefreshToken, provider.RefreshToken)
                .SetProperty(p => p.Password, provider.Password)
                .SetProperty(p => p.Rate, provider.Rate)
                .SetProperty(p => p.Salt, provider.Salt)
                .SetProperty(p => p.Role, provider.Role));
            await cache.SetStringAsync(GetCacheKey(provider.Id), JsonSerializer.Serialize(provider), CachingConfiguration.CachingConfigurationParameter);
            return provider;
        }
    }
}
