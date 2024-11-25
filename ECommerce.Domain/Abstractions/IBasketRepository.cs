using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public interface IBasketRepository
    {
        Task<int> CreateNewBasket(Basket basket);
        Task<int> DeleteBasketById(int basketId);
        Task<int> DeleteBasketByCustomerId(int customerId);
        Task<Basket> GetBasketById(int id);
        Task<Basket> GetBasketByCustomerId(int customerId);
        Task<int> AddProduct(int basketId,Product product);
        Task<int> AddProduct(int basketId, int productId);
        Task<int> AddProductRange(int basketId, params Product[] product);
        Task<int> AddProductRange(int basketId, params int[] productId);
        Task<int> RemoveProduct(int basketId, int productId);
        Task<int> RemoveProductRange(int basketId, params int[] productId);
    }
}
