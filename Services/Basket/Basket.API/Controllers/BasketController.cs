using Basket.Application.Commands;
using Basket.Application.DTOs;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/basket")]
public class BasketController : ControllerBase
{
    private readonly IMediator _mediator;

    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("{userName}")]
    public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
    {
        var query = new GetBasketByUserNameQuery(userName);
        var result = await _mediator.Send(query);

        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCartResponse>> CreateOrUpdateBasket(CreateShoppingCartDto shoppingCartDto)
    {
        var command = shoppingCartDto.ToCommand();
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpDelete("{userName}")]
    public async Task<ActionResult> DeleteBasket(string userName)
    {
        var command = new DeleteBasketByUserNameCommand(userName);
        var result = await _mediator.Send(command);

        return Ok();
    }
}
