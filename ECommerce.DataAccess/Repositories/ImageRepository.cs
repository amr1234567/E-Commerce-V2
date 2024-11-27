using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Repositories
{
    internal class ImageRepository
        (EFApplicationContext context,ILogger<ImageRepository> logger)
        : IImageRepository
    {
        public async Task<int> AssignImageForProduct(Image image, int productId)
        {
            ArgumentNullException.ThrowIfNull(image, nameof(image));
            image.ProductId = productId;
            await context.Images.AddAsync(image);
            logger.LogInformation($"new image has been ASSIGNED for product with id '{productId}'");
            return image.Id;
        }

        public async Task<int> AssignImageForProvider(Image image, int providerId)
        {
            ArgumentNullException.ThrowIfNull(image, nameof(image));
            image.ProviderId = providerId;
            await context.Images.AddAsync(image);
            logger.LogInformation($"new image has been ASSIGNED for provider with id '{providerId}'");
            return image.Id;
        }

        public async Task<int> RemoveImage(int imageId)
        {
            logger.LogInformation($"Image with id '{imageId}' has been DELETED");
            return await context.Images.Where(i => i.Id == imageId).ExecuteDeleteAsync();
        }

       
    }
}
