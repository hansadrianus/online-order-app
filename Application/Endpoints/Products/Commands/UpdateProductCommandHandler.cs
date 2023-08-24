using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Interfaces.Wrappers;
using Application.Models;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, EndpointResult<ProductViewModel>>
    {
        private readonly IRequestValidator<UpdateProductCommand> _requestValidator;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly IQueryBuilderService _queryBuilderService;
        private readonly IEntityMapperService _entityMapperService;

        public UpdateProductCommandHandler(IRequestValidator<UpdateProductCommand> requestValidator, IRepositoryWrapper repository, IMapper mapper, IQueryBuilderService queryBuilderService, IEntityMapperService entityMapperService)
        {
            _requestValidator = requestValidator;
            _repository = repository;
            _mapper = mapper;
            _queryBuilderService = queryBuilderService;
            _entityMapperService = entityMapperService;
        }

        public async Task<EndpointResult<ProductViewModel>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validations = _requestValidator.ValidateRequest(request);
                if (validations.Any())
                    return new EndpointResult<ProductViewModel>(Models.Enumerations.EndpointResultStatus.BadRequest, validations.ToArray());

                var product = await _repository.Product.GetAsync(q => q.Id == request.ID, cancellationToken);
                if (product == null)
                    return new EndpointResult<ProductViewModel>(Models.Enumerations.EndpointResultStatus.Invalid, "Data not found.");

                var productToUpdate = _mapper.Map<Product>(request);
                var updatedProduct = _entityMapperService.MapValues(product, productToUpdate);
                _repository.Product.Update(updatedProduct);
                await _repository.SaveAsync(cancellationToken);

                return new EndpointResult<ProductViewModel>(Models.Enumerations.EndpointResultStatus.Success, _mapper.Map<ProductViewModel>(updatedProduct));
            }
            catch (Exception ex)
            {
                return new EndpointResult<ProductViewModel>(Models.Enumerations.EndpointResultStatus.Error, ex.Message);
            }
        }
    }
}
