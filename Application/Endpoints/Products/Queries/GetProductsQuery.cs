using Application.Models;
using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.Products.Queries
{
    public class GetProductsQuery : IRequest<EndpointResult<IEnumerable<ProductViewModel>>>
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public short? Type { get; set; }
        public decimal? Price { get; set; }
        public decimal? Price_GreaterThan { get; set; }
        public decimal? Price_LessThan { get; set; }
        public decimal? Price_GreaterThanEqual { get; set; }
        public decimal? Price_LessThanEqual { get; set; }
    }
}
