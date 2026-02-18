using Basket.Application.Commands;
using Basket.Application.DTOs;
using Basket.Application.Responses;
using Basket.Core.Entities;

namespace Basket.Application.Mappers;

public static class ShoppingCartMapper
{

    public static ShoppingCartResponse ToResponse(this ShoppingCart shoppingCart)
    {
        return new ShoppingCartResponse(
            shoppingCart.UserName,
            shoppingCart.Items.ToResponse().ToList(),
            shoppingCart.GetToltaPrice());
    }

    // Delegate based Mapper
    public static readonly Func<ShoppingCart, ShoppingCartResponse> MapToResponse = shoppingCart => shoppingCart.ToResponse();


    public static ShoppingCart ToEntity(this CreateShoppingCartCommand command)
    {
        return new ShoppingCart(
            command.Username,
            command.Items.ToEntity().ToList());
    }

    public static CreateShoppingCartCommand ToCommand(this CreateShoppingCartDto dto)
    {
        return new CreateShoppingCartCommand(
            dto.UserName,
            dto.Items);
    }
}
