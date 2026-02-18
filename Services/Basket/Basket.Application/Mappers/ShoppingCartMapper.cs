using Basket.Application.Responses;
using Basket.Core.Entities;

namespace Basket.Application.Mappers;

public static class ShoppingCartMapper
{

    public static ShoppingCartResponse ToResponse(this ShoppingCart shoppingCart)
    {
        return new ShoppingCartResponse(
            shoppingCart.UserName,
            shoppingCart.Items.Select(i => i.ToResponse()).ToList(),
            shoppingCart.GetToltaPrice());
    }

    // Delegate based Mapper
    public static readonly Func<ShoppingCart, ShoppingCartResponse> MapToResponse = shoppingCart => shoppingCart.ToResponse();

}
