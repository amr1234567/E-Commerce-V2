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
        Task<Basket> DeleteBasketById(int basketId);
        Task<Basket> DeleteBasketByCustomerId(int customerId);
        Task<Basket> GetBasketById(int id);
        Task<Basket> GetBasketByCustomerId(int customerId);
        Task<Basket> AddProduct(Product product);
        Task<Basket> AddProduct(int productId);
        Task<Basket> AddProductRange(params Product[] product);
        Task<Basket> AddProductRange(params int[] productId);
        Task<Basket> RemoveProduct(int productId);
        Task<Basket> RemoveProductRange(params int[] productId);
    }
}
