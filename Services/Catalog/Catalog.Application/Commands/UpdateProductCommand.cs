using Catalog.Application.DTOs;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Commands;

public record class UpdateProductCommand : IRequest<bool>
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Summary { get; init; }
    public string Description { get; init; }
    public string ImageFile { get; init; }
    public string BrandId { get; init; }
    public string TypeId { get; init; }
    public int Price { get; init; }
}

public record class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = await _productRepository.GetProduct(request.Id);
        if (productEntity == null) throw new ApplicationException($"Product with ID '{request.Id}' was not found");

        var brand = await _productRepository.GetBrandById(request.BrandId);
        if (brand == null) throw new ApplicationException($"Brand with ID '{request.BrandId}' was not found.");

        var type = await _productRepository.GetTypeById(request.TypeId);
        if (type == null) throw new ApplicationException($"Type with ID '{request.TypeId}' was not found.");

        productEntity.Update(
            request.Name,
            request.Summary,
            request.Description,
            request.ImageFile,
            brand,
            type,
            request.Price);

        return await _productRepository.UpdateProduct(productEntity);
    }
}