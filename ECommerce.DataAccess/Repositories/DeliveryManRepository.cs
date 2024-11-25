using Dapper;
using System.Data;

namespace ECommerce.DataAccess.Repositories
{
    public class DeliveryManRepository
        (EFApplicationContext efContext, AppDapperContext dapperContext,
        ILogger<DeliveryManRepository> logger)
        : IDeliveryManRepository
    {
        private readonly IDbConnection dbConnection = dapperContext.CreateConnection();
        public async Task<int> AcceptDeliveryCreation(int deliveryManId)
        {
            var deliveryMan = await efContext.DeliveryMen.FirstOrDefaultAsync(d => d.Id == deliveryManId)
                ?? throw new EntityNotFoundException(typeof(DeliveryMan).Name, deliveryManId);

            deliveryMan.IsConfirmedAsDeliveryMan = true;
            logger.LogInformation($"Delivery man with id '{deliveryManId}' ACCEPTED");
            return 1;
        }

        public async Task<int> AcceptOrder(int orderId, int deliveryManId)
        {
            var orderLog = new OrderLog
            {
                DeliveryManId = deliveryManId,
                OrderId = orderId,
                ResponseType = DeliveryResponseType.Accepted,
            };
            await efContext.OrderLogs.AddAsync(orderLog);
            logger.LogInformation($"Delivery Man with id {deliveryManId} accept order '{orderId}'");
            return 1;
        }

        public async Task<List<Order>> GetOrderHistoryForDeliveryMan
            (int deliveryManId, int page = 1, int size = 10)
        {
            try
            {
                var sqlQuery = "select * from Orders where DeliveryManId = @deliveryManId Order by Name Offset @skip ROWS fetch NEXT @size ROWS ONLY;";
                var parameters = new { deliveryManId, page = page - 1, size };
                var orders = await dbConnection.QueryAsync<Order>(sqlQuery, parameters);
                return orders.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return [];
            }
        }

        public async Task<List<OrderLog>> GetOrderLogHistoryForDeliveryMan
            (int deliveryManId, int page = 1, int size = 10)
        {
            try
            {
                var sqlQuery = "select * from OrderLogs where DeliveryManId = @deliveryManId Order by Name Offset @skip ROWS fetch NEXT @size ROWS ONLY;";
                var parameters = new { deliveryManId, page = page - 1, size };
                var orderLogs = await dbConnection.QueryAsync<OrderLog>(sqlQuery, parameters);
                return orderLogs.ToList();
            }catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return [];
            }
        }

        public async Task<int> MakeAvailable(int id)
        {
            var deliveryMan = await efContext.DeliveryMen.FirstOrDefaultAsync(x => x.Id == id);
            if (deliveryMan == null)
                throw new EntityNotFoundException(typeof(DeliveryMan), id);
            deliveryMan.IsAvailable = true;
            logger.LogInformation($"Delivery man with id '{id}' is AVAILABLE");
            return 1;

        }

        public async Task<int> MakeUnavailable(int id)
        {
            var deliveryMan = await efContext.DeliveryMen.FirstOrDefaultAsync(x => x.Id == id);
            if (deliveryMan == null)
                throw new EntityNotFoundException(typeof(DeliveryMan), id);
            deliveryMan.IsAvailable = false;
            logger.LogInformation($"Delivery man with id '{id}' is UNAVAILABLE");
            return 1;
        }

        public async Task<int> RegisterAsDeliveryMan(DeliveryMan model)
        {
            ArgumentNullException.ThrowIfNull(model,nameof(model));
            var deliveryMan = await efContext.BaseUsers.FirstOrDefaultAsync(d => d.Email == model.Email);

            if (deliveryMan != null)
                throw new EntityExistsException(typeof(DeliveryMan), model.Email);

            await efContext.DeliveryMen.AddAsync(model);

            logger.LogInformation($"Delivery man with email '{model.Email}' is CREATED and under review");

            return 1;
        }

        public async Task<int> RejectDeliveryCreation(int deliveryManId)
        {
            var deliveryMan = await efContext.DeliveryMen.FirstOrDefaultAsync(d => d.Id == deliveryManId)
                 ?? throw new EntityNotFoundException(typeof(DeliveryMan).Name, deliveryManId);

            deliveryMan.IsConfirmedAsDeliveryMan = false;
            logger.LogInformation($"Delivery man with id '{deliveryManId}' REJECTED");
            return 1;
        }

        public async Task<int> RejectOrder(int orderId, int deliveryManId)
        {
            var orderLog = new OrderLog
            {
                DeliveryManId = deliveryManId,
                OrderId = orderId,
                ResponseType = DeliveryResponseType.Rejected,
            };
            await efContext.OrderLogs.AddAsync(orderLog);
            logger.LogInformation($"Delivery Man with id {deliveryManId} reject order '{orderId}'");
            return 1;
        }
    }
}
