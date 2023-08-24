using Application.Models.Enumerations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.Products.Commands
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            IEnumerable<short> enums = Enum.GetValues(typeof(ProductType)).Cast<ProductType>().Select(q => ((short)q));
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Type).NotEmpty().NotNull().Must(type => enums.Contains(type));
            RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThanOrEqualTo(0);
        }
    }
}
