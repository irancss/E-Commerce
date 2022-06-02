using ECommerce.Aggregator.Extensions;
using ECommerce.Aggregator.Models;

namespace ECommerce.Aggregator.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<BasketViewModel> GetBasketByUserNameAsync(string username)
    {
        var response = await _httpClient.GetAsync($"/api/v1/Basket/{username}");
        return await response.ReadContentAs<BasketViewModel>();
    }
}