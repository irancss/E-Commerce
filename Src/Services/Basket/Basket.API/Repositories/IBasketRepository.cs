using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userName);
        ShoppingCart GetBasket(string userName);

        Task<ShoppingCart> UpdateBasketAsync(ShoppingCart basket);
        ShoppingCart UpdateBasket(ShoppingCart basket);

        Task DeleteBasketAsync(string userName);
        void DeleteBasket(string userName);

    }
}
