using Catalog.Application.Commands;
using Catalog.Application.DTOs;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specifications;

namespace Catalog.Application.Mappers;

public static class ProductMapper
{
    public static ProductResponse ToResponse(this Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Summary = product.Summary,
            Description = product.Description,
            ImageFile = product.ImageFile,
            Price = product.Price,
            Brand = product.Brand.ToResponse(),
            Type = product.Type.ToResponse(),
            CreatedDate = product.CreatedDate,
        };
    }

    public static Pagination<ProductResponse> ToResponse(this Pagination<Product> productsPagination)
    {
        var productResponseList = productsPagination.Data.Select(p => p.ToResponse()).ToList();

        return new Pagination<ProductResponse>(
            productsPagination.PageIndex,
            productsPagination.PageSize,
            productsPagination.Count,
            productResponseList);
    }

    public static IEnumerable<ProductResponse> ToResponse(this IEnumerable<Product> products)
    {
        return products.Select(p => p.ToResponse());
    }

    public static Product ToEntity(this CreateProductCommand commnad, ProductBrand brand, ProductType type)
    {
        return new Product(
            commnad.Name,
            commnad.Summary,
            commnad.Description,
            commnad.ImageFile,
            brand,
            type,
            commnad.Price,
            DateTime.UtcNow);
    }

    public static UpdateProductCommand ToCommand(this UpdateProductDto dto, string id)
    {
        return new UpdateProductCommand
        {
            Id = id,
            Name = dto.Name,
            Summary = dto.Summary,
            Description = dto.Description,
            ImageFile = dto.ImageFile,
            BrandId = dto.BrandId,
            TypeId = dto.TypeId,
            Price = dto.Price
        };
    }

    public static CreateProductCommand ToCommand(this CreateProductDto dto)
    {
        return new CreateProductCommand
        {
            Name = dto.Name,
            Summary = dto.Summary,
            Description = dto.Description,
            ImageFile = dto.ImageFile,
            BrandId = dto.BrandId,
            TypeId = dto.TypeId,
            Price = dto.Price
        };
    }
}
