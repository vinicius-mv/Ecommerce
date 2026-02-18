using System.ComponentModel.DataAnnotations;

namespace Basket.Application.DTOs;

public record class CreateShoppingCartDto
{
    [Required]
    public string UserName { get; init; }

    public List<CreateShoppingCartItemDto> Items { get; init; } = new();
}