using ECommerce.Domain.ComplexObjects;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll(int page = 1, int size = 1);
        Task<List<Product>> GetAllByCategoryId(int categoryId,int page = 1, int size = 1);
        Task<List<Product>> GetAllByProvider(int providerId,int page = 1, int size = 1);
        Task<List<Product>> GetAllByCategoryAndProvider(int categoryId,int providerId ,int page = 1, int size = 1);
        Task<List<Product>> GetSimilarToProduct(int productId);
        Task<Product> GetProductById(int productId);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(int productId);
        Task<Product> CreateProduct(Product product);
        Task<Product> AddDiscountToProduct(int productId,ProductDiscount productDiscount);
        Task<Product> RemoveDiscountToProduct(int productId);
    }
}
