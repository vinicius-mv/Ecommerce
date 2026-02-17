using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Application.Commands;

public record class CreateProductCommand : IRequest<ProductResponse>
{
    public string Name { get; init; }
    public string Summary { get; init; }
    public string Description { get; init; }
    public string ImageFile { get; init; }
    public string BrandId { get; init; }
    public string TypeId { get; init; }
    public decimal Price { get; init; }
}

public record class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var brand = await _productRepository.GetBrandById(request.BrandId);
        if (brand == null) throw new ApplicationException($"Brand with ID '{request.BrandId}' was not found.");

        var type = await _productRepository.GetTypeById(request.TypeId);
        if (type == null) throw new ApplicationException($"Type with ID '{request.TypeId}' was not found.");

        var productEntity = request.ToEntity(brand, type);
        var newProduct = await _productRepository.CreateProduct(productEntity);

        return newProduct.ToResponse();
    }
}
