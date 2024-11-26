using Dapper;
using ECommerce.DataAccess.Constants;
using Microsoft.Extensions.Caching.Distributed;
using System.Data;
using System.Text.Json;


namespace ECommerce.DataAccess.Repositories
{
    public class CategoryRepository(EFApplicationContext efContext, AppDapperContext dapperContext
        , IDistributedCache cache)
        : ICategoryRepository
    {
        private const string CategoryCacheKey = "Category_";

        private readonly IDbConnection dbConnection = dapperContext.CreateConnection();
        public async Task<int> CreateCategory(Category category)
        {
            ArgumentNullException.ThrowIfNull(category, nameof(category));

            // ---- DB ----
            await efContext.Category.AddAsync(category);

            //--- Cashing --- 
            var cacheKey = CategoryCacheKey + category.Id;
           

            var categoryJson = JsonSerializer.Serialize(category);
            await cache.SetStringAsync(cacheKey, categoryJson, CachingConfiguration.CachingConfigurationParameter);

            return category.Id;
        }

        public async Task<int> DeleteCategory(int categoryId)
        {
            //--- Cashing --- 
            var chachKey = CategoryCacheKey + categoryId;
            await cache.RemoveAsync(chachKey);

            // ---- DB ----
            return await efContext.Category.Where(c => c.Id == categoryId).ExecuteDeleteAsync();
        }

        public async Task<Category> GetById(int id)
        {
            //--- Cashing --- 
            var cacheKey = CategoryCacheKey + id;

            var cachedCategory = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedCategory))
            {
                return JsonSerializer.Deserialize<Category>(cachedCategory)!;
            }

            // ---- DB ----
            var category = await efContext.Category.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new EntityNotFoundException(typeof(Category).Name, id);

            //--- Cashing ---
            var categoryJson = JsonSerializer.Serialize(category);
            await cache.SetStringAsync(cacheKey, categoryJson, CachingConfiguration.CachingConfigurationParameter);


            return category;
        }

        public async Task<List<Category>> GetCategories(int page = 1, int size = 10)
        {
            //--- Cashing --- 

            string cacheKey = $"categories_page_{page}_size_{size}";

            var cachedCategories = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedCategories))
            {
                return JsonSerializer.Deserialize<List<Category>>(cachedCategories);
            }

            // ---- DB ----
            var sqlQuery = "Select * from Categories Order by Name Offset @skip ROWS fetch NEXT @size ROWS ONLY";
            var parameters = new { skip = (page - 1) * size, size};
            var categories = await dbConnection.QueryAsync<Category>(sqlQuery, parameters);

            //--- Cashing --- 
            var serializedData = JsonSerializer.Serialize(categories.ToList());
            await cache.SetStringAsync(cacheKey, serializedData, CachingConfiguration.CachingConfigurationParameter);

            return categories.ToList();
        }

        public async Task<int> UpdateCategory(Category model)
        {
            // ---- DB ----
            var category = await efContext.Category.FirstOrDefaultAsync(c => c.Id == model.Id)
                ?? throw new EntityNotFoundException(typeof(Category).Name, model.Id);

            category.Name = string.IsNullOrEmpty(model.Name) ? category.Name : model.Name;
            category.Description = string.IsNullOrEmpty(model.Description) ? category.Description : model.Description;


            //--- Cashing --- 

            var cacheKey = CategoryCacheKey + category.Id;

            var categoryJson = JsonSerializer.Serialize(category);
            await cache.SetStringAsync(cacheKey, categoryJson, CachingConfiguration.CachingConfigurationParameter);

            return 1;
        }
    }
}
