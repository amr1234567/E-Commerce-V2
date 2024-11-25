using ECommerce.Domain.ComplexObjects;
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
        Task<Order> GenerateOrder(Basket basket, Address address, int TimeExpectedToDelivered, PaymentMethod paymentMethod);
        Task<int> ChangeOrderStatus(int orderId, OrderStatus orderStatus);
        Task<int> AssignOrderToDeliveryMan(int orderId, int deliveryManId, decimal deliveryPrice);
    }
}
