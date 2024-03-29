﻿using Application.Interfaces;
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
    public class DeleteSalesOrderCommandHandler : IRequestHandler<DeleteSalesOrderCommand, EndpointResult<SalesOrderViewModel>>
    {
        private readonly IRequestValidator<DeleteSalesOrderCommand> _requestValidator;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public DeleteSalesOrderCommandHandler(IRequestValidator<DeleteSalesOrderCommand> requestValidator, IRepositoryWrapper repository, IMapper mapper)
        {
            _requestValidator = requestValidator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EndpointResult<SalesOrderViewModel>> Handle(DeleteSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var validations = _requestValidator.ValidateRequest(request);
            if (validations.Any())
                return new EndpointResult<SalesOrderViewModel>(EndpointResultStatus.BadRequest, validations.ToArray());

            try
            {
                var order = _mapper.Map<SalesOrderHeader>(request);
                var orderToDelete = await _repository.SalesOrder.GetAsync(q => q.Id == order.Id && q.RowStatus == (short)DbStatus.Active, cancellationToken);
                if (orderToDelete == null)
                    return new EndpointResult<SalesOrderViewModel>(EndpointResultStatus.Invalid, "Data not found");

                orderToDelete.RowStatus = 1;
                orderToDelete.SalesOrderDetails.ToList().ForEach(q => q.RowStatus = 1);
                _repository.SalesOrder.Update(orderToDelete);
                await _repository.SaveAsync(cancellationToken);

                return new EndpointResult<SalesOrderViewModel>(EndpointResultStatus.Success, _mapper.Map<SalesOrderViewModel>(orderToDelete));
            }
            catch (Exception ex)
            {
                return new EndpointResult<SalesOrderViewModel>(EndpointResultStatus.Error, ex.Message);
            }
        }
    }
}
