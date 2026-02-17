using Catalog.Application.Commands;
using Catalog.Application.DTOs;
using Catalog.Application.Mappers;
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
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts([FromQuery] CatalogSpecParams specParams)
    {
        var query = new GetAllProductsQuery(specParams);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProduct(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);
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

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateProduct(CreateProductDto createProductDto)
    {
        var command = createProductDto.ToCommand();
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(string id)
    {
        var command = new DeleteProductByIdCommand(id);
        var result = await _mediator.Send(command);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {   
        var command = updateProductDto.ToCommand();
        var result = await _mediator.Send(command);

        if (!result) return NotFound();

        return NoContent();
    }

    [HttpGet("GetAllBrands")]
    public async Task<ActionResult<IEnumerable<BrandResponse>>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("GetAllTypes")]
    public async Task<ActionResult<IEnumerable<TypeResponse>>> GetAllTypes()
    {
        var query = new GetAllTypesQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("brand/{brandName}", Name = "GetProductsByBrandName")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByBrand(string brandName)
    {
        var query = new GetProductsByBrandQuery(brandName);
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}
