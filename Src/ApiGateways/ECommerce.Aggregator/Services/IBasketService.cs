using ECommerce.Aggregator.Models;

namespace ECommerce.Aggregator.Services;

public interface IBasketService
{
    Task<BasketViewModel> GetBasketByUserNameAsync(string username);
}