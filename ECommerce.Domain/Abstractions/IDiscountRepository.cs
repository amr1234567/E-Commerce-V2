using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public interface IDiscountRepository
    {
        Task<int> CreateDiscount(Discount discount);
        Task<Discount> GetById(int id);
        Task<Discount> UpdateDiscount(Discount discount);
        Task<Discount> DeleteDiscount(int discountId);
        Task<DiscountLog> UseDiscount(DiscountLog discountLog);
    }
}
