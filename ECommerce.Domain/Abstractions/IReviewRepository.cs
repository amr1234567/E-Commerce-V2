using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public interface IReviewRepository
    {
        Task<Review> CreateReview(Review review);
        Task<Review> DeleteReview(int reviewId);
        Task<List<Review>> GetAllReviewsForProvider(int providerId, int page = 1, int size = 10);
        Task<List<Review>> GetAllReviewsForProduct(int productId, int page = 1, int size = 10);
        Task<List<Review>> GetAllReviewsForDeliveryMan(int deliveryManId, int page = 1, int size = 10);
        Task<List<Review>> GetAllTypedReviewsForDeliveryMan(int deliveryManId,bool isGood, int page = 1, int size = 10);

    }
}
