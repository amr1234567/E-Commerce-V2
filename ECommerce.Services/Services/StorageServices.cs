using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using ECommerce.Services.Abstractions;
using ECommerce.Services.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ECommerce.Services.Services
{
    public class StorageServices : IStorageServices
    {
        public StorageServices(IOptions<CloudinaryConfig> options, ILogger<StorageServices> logger)
        {
            _cloudinaryConfig = options.Value;
            link = "cloudinary://" + _cloudinaryConfig.ApiKey + ":" + _cloudinaryConfig.ApiSecret + "@" + _cloudinaryConfig.CloudName;
            cloudinary = new Cloudinary(link);
            cloudinary.Api.Secure = true;
            _logger = logger;
        }
        private readonly CloudinaryConfig _cloudinaryConfig;
        private readonly ILogger<StorageServices> _logger;
        private readonly string link;
        private readonly Cloudinary cloudinary;

        public Task<int> DeleteImage(string path)
        {
            var deletionParams = new DeletionParams(path);
            var deletionResult = cloudinary.Destroy(deletionParams);
            _logger.LogDebug($"Image Deleting operation DONE with response {JsonSerializer.Serialize(deletionResult)}");
            return Task.FromResult(1);
        }

        public async Task<Image> SaveImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty", nameof(file));
            }

            // Read the file content into a byte array
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }


            // Convert the file content to a Base64 string
            var base64Content = Convert.ToBase64String(fileBytes);
            // Get the MIME type (e.g., image/png, image/jpeg)
            var mimeType = file.ContentType;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription($"data:{mimeType};base64,{base64Content}"),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
                PublicId = file.Name + DateTime.UtcNow.ToString(),
            };
            _logger.LogDebug($"data:{mimeType};base64,{base64Content}");
            var uploadResult = cloudinary.Upload(uploadParams);
            _logger.LogDebug($"Saving Image in cloud {JsonSerializer.Serialize(uploadResult)}");


            var myTransformation = cloudinary.Api.UrlImgUp.Transform(new Transformation()
                .Width(300).Crop("scale").Chain()
                .Effect("cartoonify"));

            var myUrl = myTransformation.BuildUrl(file.Name + DateTime.UtcNow.ToString());
            return Image.Create(myUrl);
        }
    }
}
