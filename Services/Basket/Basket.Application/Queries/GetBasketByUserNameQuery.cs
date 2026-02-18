using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Queries;

public record class GetBasketByUserNameQuery(string UserName)
    : IRequest<ShoppingCartResponse>
{
}

public record class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;

    public GetBasketByUserNameQueryHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _basketRepository.GetBasket(request.UserName);

        if (shoppingCart == null) return new ShoppingCartResponse(request.UserName);

        //return ShoppingCartMapper.MapToResponse(shoppingCart);    // example using delegate mapper
        return shoppingCart.ToResponse();
    }
}