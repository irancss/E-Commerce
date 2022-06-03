using ECommerce_Website.Models;

namespace ECommerce_Website.Services;

public interface IBasketService
{
    Task<BasketModel> GetBasket(string userName);
    Task<BasketModel> UpdateBasket(BasketModel basket);
    Task CheckoutBasket(BasketCheckoutModel basketCheckout);
}