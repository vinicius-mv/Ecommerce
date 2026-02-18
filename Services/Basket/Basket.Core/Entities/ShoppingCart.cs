namespace Basket.Core.Entities;

public class ShoppingCart
{
    public string UserName { get; private set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public ShoppingCart(string userName, List<ShoppingCartItem> items)
    {
        UserName = userName;
        Items = items;
    }

    public ShoppingCart()    // parameterless constructor for deserialization
    {
    }

    public decimal GetToltaPrice()
    {
        var totalPrice = 0.0m;
        foreach (var item in Items)
        {
            totalPrice += item.Price * item.Quantity;
        }
        return totalPrice;
    }
}
