namespace ECommerce.DataAccess.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public Task<Review> CreateReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<Review> DeleteReview(int reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllReviewsForDeliveryMan(int deliveryManId, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllReviewsForProduct(int productId, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllReviewsForProvider(int providerId, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllTypedReviewsForDeliveryMan(int deliveryManId, bool isGood, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }
    }
}
