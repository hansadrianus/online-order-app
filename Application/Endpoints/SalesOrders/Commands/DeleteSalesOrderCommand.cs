﻿using Application.Models;
using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.SalesOrders.Commands
{
    public class DeleteSalesOrderCommand : IRequest<EndpointResult<SalesOrderViewModel>>
    {
        public DeleteSalesOrderCommand(int id)
        {
            ID = id;
        }

        public int ID { get; set; }
    }
}
