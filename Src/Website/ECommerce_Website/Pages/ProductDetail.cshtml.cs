using ECommerce_Website.Models;
using ECommerce_Website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce_Website
{
    public class ProductDetailModel : PageModel
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public ProductDetailModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public CatalogModel Product { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return NotFound();
            }

            Product = await _catalogService.GetCatalogById(productId);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            var product = await _catalogService.GetCatalogById(productId);

            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });
            var userName = "swn"; // identity model

            var basket = await _basketService.GetBasket(userName);

            basket.BasketItemModels.Add(new BasketItemModel()
            {
                ProductName = product.Name,
                ProductId = productId,
                Price = product.Price,
                Color = "Black",
                Quantity = 1
            });

            var basketUpdate = await _basketService.UpdateBasket(basket);

            return RedirectToPage("Cart");
        }
    }
}