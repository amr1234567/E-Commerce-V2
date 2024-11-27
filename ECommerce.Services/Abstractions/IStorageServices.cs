using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Services.Abstractions
{
    public interface IStorageServices
    {
        Task<int> DeleteImage(string path);
        Task<Image> SaveImage(IFormFile file);
    }
}