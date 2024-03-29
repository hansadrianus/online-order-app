﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.SalesOrders.Commands
{
    public class UpdateSalesOrderCommandValidator : AbstractValidator<UpdateSalesOrderCommand>
    {
        public UpdateSalesOrderCommandValidator()
        {
            RuleFor(x => x.OrderDetails).NotEmpty().NotNull();
        }
    }
}
