using Application.Interfaces;
using Application.Interfaces.Services;
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
    public class AddSalesOrderCommandHandler : IRequestHandler<AddSalesOrderCommand, EndpointResult<SalesOrderViewModel>>
    {
        private readonly IRequestValidator<AddSalesOrderCommand> _requestValidator;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public AddSalesOrderCommandHandler(IRequestValidator<AddSalesOrderCommand> requestValidator, IRepositoryWrapper repository, IMapper mapper)
        {
            _requestValidator = requestValidator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EndpointResult<SalesOrderViewModel>> Handle(AddSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var validations = _requestValidator.ValidateRequest(request);
            if (validations.Any())
                return new EndpointResult<SalesOrderViewModel>(EndpointResultStatus.BadRequest, validations.ToArray());

            try
            {
                var salesOrder = _mapper.Map<SalesOrderHeader>(request);
                salesOrder.SalesOrderDetails = _mapper.Map<IList<SalesOrderDetail>>(request.OrderDetails);
                await _repository.SalesOrder.AddAsync(salesOrder, cancellationToken);
                await _repository.SaveAsync(cancellationToken);

                return new EndpointResult<SalesOrderViewModel>(EndpointResultStatus.Success, _mapper.Map<SalesOrderViewModel>(salesOrder));
            }
            catch (Exception ex)
            {
                return new EndpointResult<SalesOrderViewModel>(EndpointResultStatus.Error, ex.Message);
            }
        }
    }
}
