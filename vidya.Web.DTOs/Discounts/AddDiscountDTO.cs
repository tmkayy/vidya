using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Services.Mapping;

namespace vidya.Web.DTOs.Discounts
{
    public class AddDiscountDTO : IMapTo<Discount>
    {
        public int Id { get; set; }

        public decimal Percentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int GameId {  get; set; }
    }
}
