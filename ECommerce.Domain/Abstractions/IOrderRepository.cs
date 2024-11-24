using ECommerce.Domain.Enums;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public interface IOrderRepository
    {
        Task<Order> GenerateOrder(Order order);
        Task<Order> GenerateOrder(Basket basket);
        Task<int> ChangeOrderStatus(int orderId, OrderStatus orderStatus);
    }
}
