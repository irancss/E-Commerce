namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {

        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var items in ShoppingCartItems)
                {
                    totalPrice += items.Price * items.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
