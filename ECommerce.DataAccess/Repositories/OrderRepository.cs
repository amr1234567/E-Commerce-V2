using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public Task<int> ChangeOrderStatus(int orderId, OrderStatus orderStatus)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GenerateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GenerateOrder(Basket basket)
        {
            throw new NotImplementedException();
        }
    }
}
