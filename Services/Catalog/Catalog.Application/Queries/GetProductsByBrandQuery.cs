using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries;

public record class GetProductsByBrandQuery(string BrandName) 
    : IRequest<IEnumerable<ProductResponse>>
{
}

public record class GetProductsByBrandQueryHandler : IRequestHandler<GetProductsByBrandQuery, IEnumerable<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByBrandQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsByBrand(request.BrandName);
        return products.ToResponse();
    }
}