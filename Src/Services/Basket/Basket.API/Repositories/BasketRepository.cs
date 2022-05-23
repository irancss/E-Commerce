using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache cache)
    {
        _redisCache = cache;
    }


    public async Task<ShoppingCart> GetBasketAsync(string userName)
    {
        var basket = await _redisCache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket))
            return null;

        return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public ShoppingCart GetBasket(string userName)
    {
        var basket = _redisCache.GetString(userName);
        if (string.IsNullOrEmpty(basket))
            return null;

        return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket)
    {
        //this method support add , create or update
        await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
        return await GetBasketAsync(basket.UserName);
    }

    public ShoppingCart UpdateBasket(ShoppingCart basket)
    {
        _redisCache.SetString(basket.UserName, JsonConvert.SerializeObject(basket));
        return GetBasket(basket.UserName);
    }

    public async Task DeleteBasketAsync(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }

    public void DeleteBasket(string userName)
    {
        _redisCache.Remove(userName);
    }
}