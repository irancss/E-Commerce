using ECommerce.Aggregator.Extensions;
using ECommerce.Aggregator.Models;

namespace ECommerce.Aggregator.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<OrderResponseViewModel>> GetOrderByUserNameAsync(string username)
    {
        var response = await _httpClient.GetAsync($"api/v1/Order/{username}");
        return await response.ReadContentAs<List<OrderResponseViewModel>>();
    }
}