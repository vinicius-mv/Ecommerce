using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task DeleteBasket(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }

    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basketRaw = await _redisCache.GetStringAsync(userName);

        if (string.IsNullOrEmpty(basketRaw)) return null;

        var basket = JsonSerializer.Deserialize<ShoppingCart>(basketRaw);

        return basket;
    }

    public async Task<ShoppingCart> UpsertBasket(ShoppingCart shoppingCart)
    {
        var shoppingCartJson = JsonSerializer.Serialize(shoppingCart);
        await _redisCache.SetStringAsync(shoppingCart.UserName, shoppingCartJson);

        return shoppingCart;
    }
}
