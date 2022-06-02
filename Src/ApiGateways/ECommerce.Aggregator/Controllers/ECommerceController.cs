using ECommerce.Aggregator.Models;
using ECommerce.Aggregator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Aggregator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ECommerceController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ICatalogService _catalogService;
        private readonly IOrderService _orderService;

        public ECommerceController(IBasketService basketService, ICatalogService catalogService,
            IOrderService orderService)
        {
            _basketService = basketService;
            _catalogService = catalogService;
            _orderService = orderService;
        }


        [HttpGet("{userName}",Name = "GetECommerce")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(ECommerceViewModel))]
        public async Task<ActionResult<ECommerceViewModel>> GetECommerce(string userName)
        {
            //1 - get basket with username
            var basket = await _basketService.GetBasketByUserNameAsync(userName);


            // 2 - iterate basket items and consume products with basket item productId member
            foreach (var item in basket.ShoppingCartItems)
            {
                var product = await _catalogService.GetCatalogByIdAsync(item.ProductId);

                //set additional product fields into basket item
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
                item.Summary = product.Summary;
            }


            // 3- map product related members into basket item DTO with extended columns


            // consume ordering microservices in order to retrieve order list
            var order = await _orderService.GetOrderByUserNameAsync(userName);

            var eCommerceViewModel = new ECommerceViewModel()
            {
                Orders = order,
                BasketWithProducts = basket,
                UserName = userName
            };

            // return root ECommerce DTO class which including all responses
            return Ok(eCommerceViewModel);

        }
    }
}
