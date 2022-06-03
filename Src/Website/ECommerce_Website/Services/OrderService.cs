using ECommerce_Website.Extensions;
using ECommerce_Website.Models;

namespace ECommerce_Website.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string userName)
    {
        var response = await _httpClient.GetAsync($"/Order/{userName}");
        return await response.ReadContentAs<List<OrderResponseModel>>();
    }
}