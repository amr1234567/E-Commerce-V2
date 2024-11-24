using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Identity;
using ECommerce.Domain.Models;

namespace ECommerce.DataAccess.Repositories
{
    public class DeliveryManRepository : IDeliveryManRepository
    {
        public Task<int> AcceptDeliveryCreation(int deliveryManId)
        {
            throw new NotImplementedException();
        }

        public Task<int> AcceptOrder(int orderId, int deliveryManId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrderHistoryForDeliveryMan(int deliveryManId, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderLog>> GetOrderLogHistoryForDeliveryMan(int deliveryManId, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<int> MakeAvailable(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> MakeUnavailable(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> RegisterAsDeliveryMan(DeliveryMan model)
        {
            throw new NotImplementedException();
        }

        public Task<int> RejectDeliveryCreation(int deliveryManId)
        {
            throw new NotImplementedException();
        }

        public Task<int> RejectOrder(int orderId, int deliveryManId)
        {
            throw new NotImplementedException();
        }
    }
}
