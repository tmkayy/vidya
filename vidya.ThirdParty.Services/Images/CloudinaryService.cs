using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace vidya.ThirdParty.Services.Images
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            _configuration = configuration;
            var accConfig = new Account(
                _configuration["Cloudinary:CloudName"],
                _configuration["Cloudinary:ApiKey"],
                _configuration["Cloudinary:ApiSecret"]);
            _cloudinary = new Cloudinary(accConfig);
        }

        public string? UploadImage(IFormFile? file)
        {
            if (file is null) return null;

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
            };

            var uploadResult = _cloudinary.Upload(uploadParams);
            return uploadResult.Url.ToString();
        }
    }
}
