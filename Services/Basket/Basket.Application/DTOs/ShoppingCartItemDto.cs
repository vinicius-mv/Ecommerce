namespace Basket.Application.DTOs;

public record class ShoppingCartItemDto(
    string ProductId,
    string ProductName,
    string ImageFile,
    decimal Price,
    int Quantity);
