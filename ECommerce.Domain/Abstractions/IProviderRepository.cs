using ECommerce.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public interface IProviderRepository
    {
        Task<Provider> CreateProvider(Provider provider);
        Task<Provider> UpdateProvider(Provider provider);
        Task<Provider> DeleteProvider(Provider provider);
        Task<Provider> GetById(int id);
        Task<List<Provider>> GetAll(int page = 1, int size = 10);
        Task<List<Provider>> GetAllInCategory(int categoryId, int page = 1, int size = 10);
        Task<int> AddProviderToFavorite(int customerId, int providerId);
    }
}
