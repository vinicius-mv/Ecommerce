namespace Basket.Application.Responses;

public record class ShoppingCartItemResponse(
    int Quantity,
    string ImageFile,
    decimal Price,
    string ProductId,
    string ProductName);
