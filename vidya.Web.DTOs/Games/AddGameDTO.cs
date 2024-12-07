using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Services.Mapping;

namespace vidya.Web.DTOs.Games
{
    public class AddGameDTO : IMapTo<Game>
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public IFormFile? Image { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
