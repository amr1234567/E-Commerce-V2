namespace ECommerce.DataAccess.Repositories
{
    public class WishListRepository : IWishListRepository
    {
        public Task<WishList> AddProductsToWishList(int wishListId, params Product[] products)
        {
            throw new NotImplementedException();
        }

        public Task<WishList> AddProductsToWishList(int wishListId, params int[] productIds)
        {
            throw new NotImplementedException();
        }

        public Task<WishList> CreateWishList(WishList wishList)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductsFromWishList(int wishListId, params Product[] products)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductsFromWishList(int wishListId, params int[] products)
        {
            throw new NotImplementedException();
        }

        public Task<WishList> DeleteWishList(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<WishList>> GetAllForCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<WishList> GetWishList(int id)
        {
            throw new NotImplementedException();
        }

        public Task<WishList> UpdateWishList(WishList wishList)
        {
            throw new NotImplementedException();
        }
    }
}
