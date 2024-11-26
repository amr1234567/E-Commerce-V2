using Dapper;
using ECommerce.Domain.ComplexObjects;
using System.Data;

namespace ECommerce.DataAccess.Repositories
{
    public class ProductRepository
        (EFApplicationContext context,AppDapperContext dapperContext, ILogger<ProductRepository> logger)
        : IProductRepository
    {
        private readonly IDbConnection dbConnection = dapperContext.CreateConnection();
        public async Task<Product> AddDiscountToProduct(int productId, ProductDiscount productDiscount)
        {
            ArgumentNullException.ThrowIfNull(productDiscount, nameof(productDiscount));
            var product = await context.Products.FirstOrDefaultAsync(product => product.Id == productId);
            if (product == null)
                throw new EntityNotFoundException(typeof(Product), productId);
            product.ProductDiscount = productDiscount;
            logger.LogInformation($"A new discount is ASSIGNED to product with id '{productId}'");
            return product;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product, nameof(product));
            await context.Products.AddAsync(product);

            logger.LogInformation($"A new product is CREATED with id '{product.Id}'");
            return product;
        }

        public async Task<Product> DeleteProduct(int productId)
        {
            var product = await GetProductById(productId);
            context.Products.Remove(product);
            logger.LogInformation($"Product with id '{product.Id}' is DELETED");
            return product;
        }

        public async Task<List<Product>> GetAll(int page = 1, int size = 10)
        {
            var sqlQuery = "Select * from Products Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";
            var parameters = new { skip = (page - 1) * size, size };
            var products = await dbConnection.QueryAsync<Product>(sqlQuery, parameters);
            logger.LogDebug($"{products.Count()} rows has been SELECTED");
            //var products = await context.Products.Skip(page - 1).Take(size).ToListAsync();
            return products.ToList();
        }

        public async Task<List<Product>> GetAllByCategoryAndProvider(int? categoryId, int? providerId, int page = 1, int size = 10)
        {
            //var products = context.Products.AsNoTracking();
            var sqlQuery = "";
            var parameters = new Dictionary<string, object>()
            {
                {"@skip", (page - 1) * size },
                {"@size",size }
            };
            logger.LogDebug($"In {nameof(GetAllByCategoryAndProvider)} function, categoryId = '{categoryId}' - providerId = '{providerId}'");

            if (categoryId != null && providerId is null)
            {
                sqlQuery = "Select * from Products where CategoryId = @categoryId  Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";
                parameters.Add("@categoryId", categoryId);
                //products = products.Where(p => p.CategoryId == categoryId);

            }
            else if (providerId != null && categoryId is null)
            {
                //products = products.Where(p => p.ProviderId == providerId);
                sqlQuery = "Select * from Products where ProviderId = @providerId Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";
                parameters.Add("@providerId", providerId);
            }
            else if (providerId != null && categoryId is not null)
            {
                sqlQuery = "Select * from Products where ProviderId = @providerId and CategoryId = @categoryId Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";
                parameters.Add("@providerId", providerId);
                parameters.Add("@categoryId", categoryId);
            }
            else if (providerId == null && categoryId is null)
            {
                sqlQuery = "Select * from Products Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";
            }

            logger.LogDebug($"final sql query is '{sqlQuery}'");

            //return await products.Skip(page - 1).Skip(size).ToListAsync();
            var products = await dbConnection.QueryAsync<Product>(sqlQuery, parameters);

            return products.ToList();
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == productId);
            if (product != null)
                logger.LogDebug($"Product {productId} FOUND In function {nameof(GetProductById)}");
            return product ?? throw new EntityNotFoundException(typeof(Product), productId);
        }

        public Task<List<Product>> GetSimilarToProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> RemoveDiscountFromProduct(int productId)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if(product == null)
            {
                logger.LogDebug($"product with id '{productId}' was NOT FOUND");
                throw new EntityNotFoundException(typeof(Product), productId);
            }
            product.ProductDiscount = null;
            logger.LogInformation($"Product with id '{product.Id}' UPDATED");
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            await context.Products.Where(p => p.Id == product.Id)
                .ExecuteUpdateAsync(p =>
                    p.SetProperty(prod => prod.AmountAvailable, product.AmountAvailable)
                    .SetProperty(prod => prod.ProductDiscount, product.ProductDiscount)
                    .SetProperty(prod => prod.Name, product.Name)
                    .SetProperty(prod => prod.Description, product.Description)
                    .SetProperty(prod => prod.Price, product.Price)
                    .SetProperty(prod => prod.Rate, product.Rate)
                    .SetProperty(prod => prod.IsAvailable, product.IsAvailable));
            logger.LogInformation($"Product with id '{product.Id}' UPDATED");
            return product;
        }
    }
}
