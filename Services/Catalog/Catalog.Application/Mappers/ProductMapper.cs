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
}
