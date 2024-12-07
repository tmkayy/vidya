using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.ThirdParty.Services.Images
{
    public interface ICloudinaryService
    {
        string? UploadImage(IFormFile? file);
    }
}
