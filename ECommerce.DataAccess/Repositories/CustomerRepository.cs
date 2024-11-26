namespace ECommerce.DataAccess.Repositories
{
    public class CustomerRepository
        (EFApplicationContext context,IUserRepository userRepository, ILogger<CustomerRepository> logger)
        : ICustomerRepository
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

        public async Task<int> AddProviderToFavorite(int customerId, int providerId)
        {
            var user = await userRepository.GetUserById(providerId);
            
            if (user == null || user is not Provider)
                throw new EntityNotFoundException(typeof(Provider), providerId);
            logger.LogDebug($"Provider with id '{providerId}' FOUND");

            var customer = await context.Customers
                .Include(c => c.FavProviders!.Where(p => p.Id == providerId))
                .FirstOrDefaultAsync(x => x.Id == customerId)
                ?? throw new EntityNotFoundException(typeof(Customer), customerId);

            logger.LogDebug($"Customer with id '{customerId}' FOUND");

            customer.FavProviders ??= [];
            customer.FavProviders.Add((Provider)user);
            return customer.Id;
        }
    }
}
