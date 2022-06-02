namespace ECommerce.Aggregator.Models;

public class BasketViewModel
{
    public string UserName { get; set; }
    public List<BasketItemExtendedViewModel> ShoppingCartItems { get; set; } = new List<BasketItemExtendedViewModel>();

    public decimal TotalPrice { get; set; }


}