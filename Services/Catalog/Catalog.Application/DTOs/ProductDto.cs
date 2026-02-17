namespace Catalog.Application.DTOs;

public record class ProductDto(
    string Id,
    string Name,
    string Summary,
    string Description,
    string ImageFile,
    BrandDto Brand,
    TypeDto Type,
    decimal Price,
    DateTimeOffset CreatedDate);