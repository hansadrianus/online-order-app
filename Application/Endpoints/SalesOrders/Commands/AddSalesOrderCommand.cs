using Application.Models;
using Application.ViewModels;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.SalesOrders.Commands
{
    public class AddSalesOrderCommand : IRequest<EndpointResult<SalesOrderViewModel>>
    {
        public string OrderNumber { get; set; }
        public ICollection<OrderDetailCommand> OrderDetails { get; set; }
    }
}
