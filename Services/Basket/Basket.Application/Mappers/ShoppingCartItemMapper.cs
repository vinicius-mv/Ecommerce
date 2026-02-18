using Basket.Application.DTOs;
using Basket.Application.Responses;
using Basket.Core.Entities;

namespace Basket.Application.Mappers;

public static class ShoppingCartItemMapper
{
    public static ShoppingCartItemResponse ToResponse(this ShoppingCartItem shoppingCartItem)
    {
        return new ShoppingCartItemResponse(
            shoppingCartItem.Quantity,
            shoppingCartItem.ImageFile,
            shoppingCartItem.Price,
            shoppingCartItem.ProductId,
            shoppingCartItem.ProductName);
    }

    public static IEnumerable<ShoppingCartItemResponse> ToResponse(this IEnumerable<ShoppingCartItem> shoppingCartItems)
    {
        return shoppingCartItems.Select(i => i.ToResponse());
    }

    public static ShoppingCartItem ToEntity(this CreateShoppingCartItemDto itemDto)
    {
        return new ShoppingCartItem(
            itemDto.Quantity,
            itemDto.Price,
            itemDto.ProductId,
            itemDto.ProductName,
            itemDto.ImageFile);
    }

    public static IEnumerable<ShoppingCartItem> ToEntity(this IEnumerable<CreateShoppingCartItemDto> itemDtos)
    {
        return itemDtos.Select(i => i.ToEntity());
    }
}
