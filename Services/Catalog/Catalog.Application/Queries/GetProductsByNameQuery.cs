using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries;

public record class GetProductsByNameQuery(string ProductName) : IRequest<IEnumerable<ProductResponse>>
{
}

public record GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, IEnumerable<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByNameQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResponse>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsByName(request.ProductName);
        return products.ToResponse();
    }
}