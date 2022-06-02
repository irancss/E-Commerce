using ECommerce.Aggregator.Models;

namespace ECommerce.Aggregator.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseViewModel>> GetOrderByUserNameAsync(string username);

}