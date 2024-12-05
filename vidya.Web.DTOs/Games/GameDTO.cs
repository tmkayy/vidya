﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.Web.DTOs.Games
{
    public class GameDTO
    {
        public int Id { get; set; }

        public string? ImageUrl { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
