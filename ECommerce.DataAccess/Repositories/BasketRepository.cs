using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public Task<Basket> AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> AddProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> AddProductRange(params Product[] product)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> AddProductRange(params int[] productId)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateNewBasket(Basket basket)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> DeleteBasketByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> DeleteBasketById(int basketId)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> GetBasketByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> GetBasketById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> RemoveProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> RemoveProductRange(params int[] productId)
        {
            throw new NotImplementedException();
        }
    }
}
