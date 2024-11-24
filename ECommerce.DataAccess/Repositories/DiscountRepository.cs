using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        public Task<int> CreateDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }

        public Task<Discount> DeleteDiscount(int discountId)
        {
            throw new NotImplementedException();
        }

        public Task<Discount> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Discount> UpdateDiscount(Discount discount)
        {
            throw new NotImplementedException();
        }

        public Task<DiscountLog> UseDiscount(DiscountLog discountLog)
        {
            throw new NotImplementedException();
        }
    }
}
