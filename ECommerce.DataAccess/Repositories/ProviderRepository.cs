using ECommerce.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Models;

namespace ECommerce.DataAccess.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        public Task<int> AddProviderToFavorite(int customerId, int providerId)
        {
            throw new NotImplementedException();
        }

        public Task<Provider> CreateProvider(Provider provider)
        {
            throw new NotImplementedException();
        }

        public Task<Provider> DeleteProvider(Provider provider)
        {
            throw new NotImplementedException();
        }

        public Task<List<Provider>> GetAll(int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<List<Provider>> GetAllInCategory(int categoryId, int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<Provider> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Provider> UpdateProvider(Provider provider)
        {
            throw new NotImplementedException();
        }
    }
}
