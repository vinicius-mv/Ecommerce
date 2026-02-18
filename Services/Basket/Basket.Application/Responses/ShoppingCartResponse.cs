namespace Basket.Application.Responses;

public record class ShoppingCartResponse
{
    public string UserName { get; init; }
    public List<ShoppingCartItemResponse> Items { get; init; } = new();

    public decimal TotalPrice { get; init; }

    public ShoppingCartResponse(string userName, List<ShoppingCartItemResponse> items, decimal totalPrice)
    {
        UserName = userName;
        Items = items;
        TotalPrice = totalPrice;
    }

    public ShoppingCartResponse(string userName)
    {
        UserName = userName;
    }
}
