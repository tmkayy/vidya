using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Common;
using vidya.Data.Models;
using vidya.Services.Mapping;

namespace vidya.Web.DTOs.Discounts
{
    public class AddDiscountDTO : IMapTo<Discount>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Range(GlobalConstants.DiscountMin,  GlobalConstants.DiscountMax)]
        public decimal Percentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int GameId {  get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AddDiscountDTO, Discount>().ForMember(d => d.Id, opt => opt.Ignore());
        }
    }
}
