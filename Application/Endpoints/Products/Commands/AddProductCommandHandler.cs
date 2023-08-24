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
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, EndpointResult<ProductViewModel>>
    {
        private readonly IRequestValidator<AddProductCommand> _requestValidator;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly IEntityMapperService _entityMapperService;

        public AddProductCommandHandler(IRequestValidator<AddProductCommand> requestValidator, IRepositoryWrapper repository, IMapper mapper, IEntityMapperService entityMapperService)
        {
            _requestValidator = requestValidator;
            _repository = repository;
            _mapper = mapper;
            _entityMapperService = entityMapperService;
        }

        public async Task<EndpointResult<ProductViewModel>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validations = _requestValidator.ValidateRequest(request);
                if (validations.Any())
                    return new EndpointResult<ProductViewModel>(Models.Enumerations.EndpointResultStatus.BadRequest, validations.ToArray());

                Product product = _mapper.Map<Product>(request);
                await _repository.Product.AddAsync(product, cancellationToken);
                await _repository.SaveAsync(cancellationToken);

                return new EndpointResult<ProductViewModel>(Models.Enumerations.EndpointResultStatus.Success, _mapper.Map<ProductViewModel>(product));
            }
            catch (Exception ex)
            {
                return new EndpointResult<ProductViewModel>(Models.Enumerations.EndpointResultStatus.Error, ex.Message);
            }
        }
    }
}
