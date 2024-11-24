using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public interface ICategoryRepository
    {
        Task<int> CreateCategory(Category category);
        Task<Category> GetById(int id);
        Task<List<Category>> GetCategories(int page = 1, int size = 10);
        Task<Category> UpdateCategory(Category category);
        Task<Category> DeleteCategory(int categoryId);
    }
}
