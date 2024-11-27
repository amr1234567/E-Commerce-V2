using Dapper;
using ECommerce.Domain.Identity;
using System.Data;
using System.Data.Common;

namespace ECommerce.DataAccess.Repositories
{
    public class ReviewRepository
        (EFApplicationContext context,AppDapperContext dapperContext,ILogger<ReviewRepository> logger)
        : IReviewRepository
    {
        private readonly IDbConnection dbConnection = dapperContext.CreateConnection();
        public async Task<Review> CreateReview(Review review)
        {
            ArgumentNullException.ThrowIfNull(review, nameof(review));
            await context.Reviews.AddAsync(review);
            logger.LogDebug($"new review has been CREATED with id '{review.Id}'");
            return review;
        }

        public async Task<int> DeleteReview(int reviewId)
        {
            logger.LogDebug($"new review has been DELETED with id '{reviewId}'");
            return await context.Reviews.Where(r => r.Id == reviewId).ExecuteDeleteAsync();
        }

        public async Task<List<Review>> GetAllReviewsForDeliveryMan(int deliveryManId, int page = 1, int size = 10)
        {
            var sqlQuery = "Select * from Reviews where DeliveryManId = @deliveryManId Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";
            var parameters = new { deliveryManId, skip = (page - 1) * size, size };
            var reviews = await dbConnection.QueryAsync<Review>(sqlQuery, parameters);
            return reviews.ToList();
        }

        public async Task<List<Review>> GetAllReviewsForProduct(int productId, int page = 1, int size = 10)
        {
            var sqlQuery = "Select * from Reviews where ProductId = @productId Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";
            var parameters = new { productId, skip = (page - 1) * size, size };
            var reviews = await dbConnection.QueryAsync<Review>(sqlQuery, parameters);
            return reviews.ToList();
        }

        public async Task<List<Review>> GetAllReviewsForProvider(int providerId, int page = 1, int size = 10)
        {
            var sqlQuery = "Select * from Reviews where ProviderId = @providerId Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";
            var parameters = new { providerId, skip = (page - 1) * size, size };
            var reviews = await dbConnection.QueryAsync<Review>(sqlQuery, parameters);
            return reviews.ToList();
        }

        public async Task<List<Review>> GetAllTypedReviewsForDeliveryMan(int deliveryManId, bool isGood, int page = 1, int size = 10)
        {
            var sqlQuery = "";
            var goodRate = 2.5f;
            if (isGood)
                sqlQuery = "Select * from Reviews where DeliveryManId = @deliveryManId and Rate >= @goodRate Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";
            else
                sqlQuery = "Select * from Reviews where DeliveryManId = @deliveryManId and Rate < @goodRate Order by Rate Offset @skip ROWS fetch NEXT @size ROWS ONLY";
            var parameters = new { deliveryManId, goodRate, skip = (page - 1) * size, size };
            var reviews = await dbConnection.QueryAsync<Review>(sqlQuery, parameters);
            return reviews.ToList();
        }
    }
}
