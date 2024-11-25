using ECommerce.DataAccess.EFContext;
using ECommerce.DataAccess.Exceptions;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repositories
{
    public class CustomerRepository(EFApplicationContext context) : ICustomerRepository
    {
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            ArgumentNullException.ThrowIfNull(customer, nameof(customer));
            var dbCustomer = await context.BaseUsers.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == customer.Email);

            if (dbCustomer != null)
                throw new EntityExistsException(typeof(Customer).Name, customer.Email);

            await context.Customers.AddAsync(customer);
            return customer;
        }
    }
}
