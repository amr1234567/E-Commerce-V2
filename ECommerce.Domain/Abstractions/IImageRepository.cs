using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Abstractions
{
    public interface IImageRepository
    {
        Task<int> AssignImageForProvider(Image image, int providerId);
        Task<int> RemoveImageFromProvider(int imageId, int providerId);
        Task<int> AssignImageForProduct(Image image, int productId);
        Task<int> RemoveImageFromProduct(int imageId, int productId);
    }
}
