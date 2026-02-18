namespace Basket.Application.DTOs;

public record class CreateShoppingCartItemDto(
    string ProductId,
    string ProductName,
    string ImageFile,
    decimal Price,
    int Quantity);
