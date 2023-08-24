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

namespace Application.Endpoints.Products.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, EndpointResult<IEnumerable<ProductViewModel>>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly IQueryBuilderService _queryBuilderService;

        public GetProductsQueryHandler(IRepositoryWrapper repository, IMapper mapper, IQueryBuilderService queryBuilderService)
        {
            _repository = repository;
            _mapper = mapper;
            _queryBuilderService = queryBuilderService;
        }

        public async Task<EndpointResult<IEnumerable<ProductViewModel>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var queryExpression = _queryBuilderService.BuildPredicate<Product, GetProductsQuery>(request);
                var products = await _repository.Product.GetAllAsync(queryExpression, cancellationToken);

                return new EndpointResult<IEnumerable<ProductViewModel>>(Models.Enumerations.EndpointResultStatus.Success, _mapper.Map<ProductViewModel[]>(products.Where(q => q.RowStatus == (short)DbStatus.Active)));
            }
            catch (Exception ex)
            {
                return new EndpointResult<IEnumerable<ProductViewModel>>(Models.Enumerations.EndpointResultStatus.Error, ex.Message);
            }
        }
    }
}
