namespace Basket.Application.DTOs;

public record class ShoppingCartDto(
    string UserName,
    List<ShoppingCartItemDto> Items);
