using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Interfaces.Wrappers;
using Application.Models;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Endpoints.Products.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, EndpointResult<ProductViewModel>>
    {
        private readonly IRequestValidator<DeleteProductCommand> _requestValidator;
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repository;

        public DeleteProductCommandHandler(IRequestValidator<DeleteProductCommand> requestValidator, IMapper mapper, IRepositoryWrapper repository)
        {
            _requestValidator = requestValidator;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<EndpointResult<ProductViewModel>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validations = _requestValidator.ValidateRequest(request);
                if (validations.Any())
                    return new EndpointResult<ProductViewModel>(Models.Enumerations.EndpointResultStatus.BadRequest, validations.ToArray());

                var product = await _repository.Product.GetAsync(q => q.Id == request.ID, cancellationToken);
                if (product == null)
                    return new EndpointResult<ProductViewModel>(Models.Enumerations.EndpointResultStatus.Invalid, "Data not found.");

                product.RowStatus = -1;
                _repository.Product.Update(product);
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
