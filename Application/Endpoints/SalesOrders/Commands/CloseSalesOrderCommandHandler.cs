using Application.Interfaces;
using Application.Interfaces.Wrappers;
using Application.Models;
using Application.Models.Enumerations;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.SalesOrders.Commands
{
    public class CloseSalesOrderCommandHandler : IRequestHandler<CloseSalesOrderCommand, EndpointResult<SalesOrderViewModel>>
    {
        private readonly IRequestValidator<CloseSalesOrderCommand> _requestValidator;
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public CloseSalesOrderCommandHandler(IRequestValidator<CloseSalesOrderCommand> requestValidator, IMapper mapper, IRepositoryWrapper repository)
        {
            _requestValidator = requestValidator;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<EndpointResult<SalesOrderViewModel>> Handle(CloseSalesOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validations = _requestValidator.ValidateRequest(request);
                if (validations.Any())
                    return new EndpointResult<SalesOrderViewModel>(Models.Enumerations.EndpointResultStatus.BadRequest, validations.ToArray());

                var salesOrder = await _repository.SalesOrder.GetAsync(q => q.Id == request.ID, cancellationToken);
                if (salesOrder == null)
                    return new EndpointResult<SalesOrderViewModel>(Models.Enumerations.EndpointResultStatus.Invalid, "Data not found.");

                salesOrder.Status = (short)SalesOrderStatus.Close;
                Billing billing = _mapper.Map<Billing>(request);
                billing.TotalAmount = salesOrder.SalesOrderDetails.Sum(q => q.Price);
                if (billing.PayAmount < billing.TotalAmount)
                    return new EndpointResult<SalesOrderViewModel>(Models.Enumerations.EndpointResultStatus.Invalid, "Insufficient payment.");

                billing.ChangeAmount = billing.TotalAmount - billing.PayAmount;

                _repository.SalesOrder.Update(salesOrder);
                _repository.Billing.Add(billing);
                await _repository.SaveAsync(cancellationToken);

                return new EndpointResult<SalesOrderViewModel>(Models.Enumerations.EndpointResultStatus.Success, _mapper.Map<SalesOrderViewModel>(salesOrder));
            }
            catch (Exception ex)
            {
                return new EndpointResult<SalesOrderViewModel>(Models.Enumerations.EndpointResultStatus.Error, ex.Message);
            }
        }
    }
}
