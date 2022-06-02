namespace ECommerce.Aggregator.Models;

public class ECommerceViewModel
{
    public string UserName { get; set; }
    public BasketViewModel BasketWithProducts { get; set; }
    public IEnumerable<OrderResponseViewModel> Orders { get; set; }
}