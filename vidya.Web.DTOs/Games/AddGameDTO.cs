using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Common;
using vidya.Data.Models;
using vidya.Services.Mapping;

namespace vidya.Web.DTOs.Games
{
    public class AddGameDTO : IMapTo<Game>
    {
        [Required]
        [MaxLength(GlobalConstants.GameNameMaxLength)]
        [MinLength(GlobalConstants.GameNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(GlobalConstants.GameDescriptionMaxLength)]
        [MinLength(GlobalConstants.GameDescriptionMinLength)]
        public string Description { get; set; } = null!;

        public IFormFile? Image { get; set; }

        [Required]
        [Range(GlobalConstants.GamePriceMin, GlobalConstants.GamePriceMax)]
        public decimal Price { get; set; }
    }
}
