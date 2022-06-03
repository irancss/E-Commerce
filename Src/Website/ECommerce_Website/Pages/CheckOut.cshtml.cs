using ECommerce_Website.Models;
using ECommerce_Website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce_Website
{
    public class CheckOutModel : PageModel
    {
        private readonly IBasketService _basketService;
        private readonly OrderService _orderService;

        public CheckOutModel(IBasketService basketService, OrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "swn";
            Cart = await _basketService.GetBasket(userName);
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            var userName = "swn";
            Cart = await _basketService.GetBasket("test");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            Order.UserName = userName;
            Order.TotalPrice = Cart.TotalPrice;

            await _basketService.CheckoutBasket(Order);


            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}