using ECommerce.Domain.Abstractions;
using ECommerce.Domain.ComplexObjects;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repositories
{
    public class OrderRepository(EFApplicationContext context,ILogger<OrderRepository> logger) : IOrderRepository
    {
        public async Task<int> AssignOrderToDeliveryMan(int orderId, int deliveryManId, decimal deliveryPrice)
        {
            var result = await context.Orders.Where(order => order.Id == orderId)
                .ExecuteUpdateAsync(p =>
                    p.SetProperty(o => o.DeliveryManId, deliveryManId)
                    .SetProperty(o => o.DeliveryPrice, deliveryPrice)
                    .SetProperty(o => o.OrderStatus, OrderStatus.Pending));
            logger.LogInformation($"Order with id '{orderId}' has been ASSIGNED To delivery man with id '{deliveryManId}' with price = {deliveryPrice}$");
            return result;
        }

        public async Task<int> ChangeOrderStatus(int orderId, OrderStatus orderStatus)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == orderId)
                ?? throw new EntityNotFoundException(typeof(Order), orderId);
            order.OrderStatus = orderStatus;

            return order.Id;
        }

        public async Task<Order> GenerateOrder(Order order)
        {
            ArgumentNullException.ThrowIfNull(order, nameof(order));
            await context.Orders.AddAsync(order);
            logger.LogInformation($"New Order has been GENERATED with id '{order.Id}'");
            return order;
        }

        public async Task<Order> GenerateOrder
            (Basket basket, Address address,int TimeExpectedToDelivered, PaymentMethod paymentMethod)
        {
            ArgumentNullException.ThrowIfNull(basket, nameof(basket));
            ArgumentNullException.ThrowIfNull(address, nameof(address));

            var newOrder = Order.GenerateFromBasket(basket);
            newOrder.Address = address;
            newOrder.TimeExpectedToDelivered = TimeExpectedToDelivered;
            newOrder.PaymentMethod = paymentMethod;
            await context.Orders.AddAsync(newOrder);

            logger.LogInformation($"New Order has been GENERATED with id '{newOrder.Id}'");
            return newOrder;
        }
    }
}
