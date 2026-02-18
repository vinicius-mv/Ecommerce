using Basket.Application.DTOs;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Commands;

public record class CreateShoppingCartCommand(
    string Username,
    List<CreateShoppingCartItemDto> Items)
    : IRequest<ShoppingCartResponse>
{
}

public record class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;

    public CreateShoppingCartCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart shoppingCartEntity = request.ToEntity();

        var updatedCart = await _basketRepository.UpsertBasket(shoppingCartEntity);

        return updatedCart.ToResponse();
    }
}
