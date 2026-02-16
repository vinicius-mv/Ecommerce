using Catalog.Application.DTOs;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/v1/catalog")]
public class CatalogController : ControllerBase
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts(CatalogSpecParams specParams)
    {
        var query = new GetAllProductsQuery(specParams);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProduct(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("productName/{productName}")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductByProductName(string productName)
    {
        var query = new GetProductsByNameQuery(productName);
        var result = await _mediator.Send(query);

        if (result == null || !result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }
}
