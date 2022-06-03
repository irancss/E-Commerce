using ECommerce_Website.Models;
using ECommerce_Website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce_Website
{
    public class ProductModel : PageModel
    {
  
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public ProductModel(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();


        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var productList = await _catalogService.GetAllCatalog();
            CategoryList = productList.Select(c => c.Category).Distinct();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                productList = productList.Where(c => c.Category == categoryName);
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = productList;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });

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

            return RedirectToPage("Cart");
        }
    }
}