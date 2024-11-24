using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public interface IWishListRepository
    {
        Task<WishList> CreateWishList(WishList wishList);
        Task<WishList> DeleteWishList(int id);
        Task<WishList> UpdateWishList(WishList wishList);
        Task<WishList> GetWishList(int id);
        Task<List<WishList>> GetAllForCustomer(int customerId);
        Task<WishList> AddProductsToWishList(int wishListId, params Product[] products);
        Task<WishList> AddProductsToWishList(int wishListId, params int[] productIds);
        Task DeleteProductsFromWishList(int wishListId, params Product[] products);
        Task DeleteProductsFromWishList(int wishListId, params int[] products);
    }
}
