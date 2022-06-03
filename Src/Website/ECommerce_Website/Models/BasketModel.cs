namespace ECommerce_Website.Models
{
    public class BasketModel
    {
        public string UserName { get; set; }
        public List<BasketItemModel> BasketItemModels { get; set; } = new List<BasketItemModel>();

        public decimal TotalPrice { get; set; }

    }
}
