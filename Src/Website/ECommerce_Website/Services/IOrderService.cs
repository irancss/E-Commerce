using ECommerce_Website.Models;

namespace ECommerce_Website.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string userName);
}