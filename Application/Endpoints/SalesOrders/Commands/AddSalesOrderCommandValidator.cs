using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.SalesOrders.Commands
{
    public class AddSalesOrderCommandValidator : AbstractValidator<AddSalesOrderCommand>
    {
        public AddSalesOrderCommandValidator()
        {
            RuleFor(x => x.OrderNumber).NotEmpty().NotNull();
            RuleFor(x => x.OrderDetails).NotEmpty().NotNull();
        }
    }
}
