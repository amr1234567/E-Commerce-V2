using ECommerce.Domain.Identity;
using ECommerce.Domain.Models;

namespace ECommerce.Domain.Abstractions
{
    public interface IDeliveryManRepository
    {
        Task<int> RegisterAsDeliveryMan(DeliveryMan model);
        Task<int> AcceptDeliveryCreation(int deliveryManId);
        Task<int> RejectDeliveryCreation(int deliveryManId);
        Task<int> MakeUnavailable(int id);
        Task<int> MakeAvailable(int id);
        Task<int> AcceptOrder(int orderId, int deliveryManId);
        Task<int> RejectOrder(int orderId, int deliveryManId);
        Task<List<OrderLog>> GetOrderLogHistoryForDeliveryMan(int deliveryManId, int page = 1, int size = 10);
        Task<List<Order>> GetOrderHistoryForDeliveryMan(int deliveryManId, int page = 1, int size = 10);
    }
}
