using ECommerce_Website.Extensions;
using ECommerce_Website.Models;

namespace ECommerce_Website.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BasketModel> GetBasket(string userName)
    {
        var response = await _httpClient.GetAsync($"/Basket/{userName}");
        return await response.ReadContentAs<BasketModel>();
    }

    public async Task<BasketModel> UpdateBasket(BasketModel basket)
    {
        var response = await _httpClient.PostAsJson($"/Basket", basket);
        if (response.IsSuccessStatusCode)
        {
            return await response.ReadContentAs<BasketModel>();
        }
        throw new Exception("Something went wrong when calling api");
    }

    public async Task CheckoutBasket(BasketCheckoutModel basketCheckout)
    {
        var response = await _httpClient.PostAsJson($"/Basket/Checkout", basketCheckout);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Something went wrong when calling api");
        }
    }
}