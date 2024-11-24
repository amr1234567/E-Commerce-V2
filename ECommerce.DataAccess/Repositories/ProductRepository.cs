using ECommerce.Domain.Abstractions;
using ECommerce.Domain.ComplexObjects;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Task<Product> AddDiscountToProduct(int productId, ProductDiscount productDiscount)
        {
            throw new NotImplementedException();
        }

        public Task<Product> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product> DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAll(int page = 1, int size = 1)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllByCategoryAndProvider(int categoryId, int providerId, int page = 1, int size = 1)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllByCategoryId(int categoryId, int page = 1, int size = 1)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllByProvider(int providerId, int page = 1, int size = 1)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetSimilarToProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> RemoveDiscountToProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
