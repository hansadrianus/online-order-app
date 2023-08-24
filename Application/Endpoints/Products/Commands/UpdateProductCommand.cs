using Application.Models;
using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.Products.Commands
{
    public class UpdateProductCommand : IRequest<EndpointResult<ProductViewModel>>
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public short Type { get; set; }
        public decimal Price { get; set; }
    }
}
