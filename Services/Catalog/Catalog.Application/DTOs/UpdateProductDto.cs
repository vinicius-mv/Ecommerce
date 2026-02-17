using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Application.DTOs;

public record class UpdateProductDto
{
    [Required]
    public string Name { get; init; }

    [Required]
    public string Summary { get; init; }

    [Required]
    public string Description { get; init; }

    [Required]
    public string ImageFile { get; init; }

    [Required]
    public string BrandId { get; init; }

    [Required]
    public string TypeId { get; init; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; init; }
}
