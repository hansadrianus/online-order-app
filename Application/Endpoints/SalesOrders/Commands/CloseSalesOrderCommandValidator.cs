using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.SalesOrders.Commands
{
    public class CloseSalesOrderCommandValidator : AbstractValidator<CloseSalesOrderCommand>
    {
        public CloseSalesOrderCommandValidator()
        {
            RuleFor(x => x.PayAmount).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
