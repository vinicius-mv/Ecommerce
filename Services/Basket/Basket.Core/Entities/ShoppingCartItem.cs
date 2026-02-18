namespace Basket.Core.Entities;

public class ShoppingCartItem
{
    public int Quantity { get; init; }
    public decimal Price { get; init; }
    public string ProductId { get; init; }
    public string ProductName { get; init; }
    public string ImageFile { get; init; }

    public ShoppingCartItem(int quantity, decimal price, string productId, string productName, string imageFile)
    {
        Quantity = quantity;
        Price = price;
        ProductId = productId;
        ProductName = productName;
        ImageFile = imageFile;
    }
}
