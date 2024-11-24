using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<Customer> CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
