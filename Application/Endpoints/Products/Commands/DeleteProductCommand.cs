﻿using Application.Models;
using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.Products.Commands
{
    public class DeleteProductCommand : IRequest<EndpointResult<ProductViewModel>>
    {
        public DeleteProductCommand(int id)
        {
            ID = id;
        }

        public int? ID { get; set; }
    }
}
