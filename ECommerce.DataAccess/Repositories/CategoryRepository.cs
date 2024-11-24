using ECommerce.Domain.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository

    {
        public Task<int> CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<Category> DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetCategories(int page = 1, int size = 10)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
