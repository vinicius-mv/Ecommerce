namespace Basket.Core.Entities;

public class ShoppingCart
{
    public string UserName { get; private set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    protected ShoppingCart()    // constructor for rehydration
    {
    }
}
