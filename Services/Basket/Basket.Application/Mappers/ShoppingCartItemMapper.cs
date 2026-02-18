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
}
