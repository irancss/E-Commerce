using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ILogger<BasketController> _logger;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _eventBus;

        public BasketController(IBasketRepository basketRepository, ILogger<BasketController> logger, DiscountGrpcService discountGrpcService, IMapper mapper, IPublishEndpoint eventBus)
        {
            _basketRepository = basketRepository;
            _logger = logger;
            _discountGrpcService = discountGrpcService;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCart))]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _basketRepository.GetBasketAsync(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCart))]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            foreach (var item in shoppingCart.ShoppingCartItems)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }


            return Ok(await _basketRepository.UpdateBasketAsync(shoppingCart));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCart))]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            try
            {
                await _basketRepository.DeleteBasketAsync(userName);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            //1 get existing basket with total price
            var basket = await _basketRepository.GetBasketAsync(basketCheckout.UserName);
            if (basket == null)
            {
                return BadRequest();
            }

            //2 Create Basket Checkout Event --- set total price on basketCheckout eventMessage  
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;


            //3 Send Checkout Event ro rabbitMQ
            await _eventBus.Publish(eventMessage);


            //4 remove the basket
            await _basketRepository.DeleteBasketAsync(basketCheckout.UserName);

            return Accepted();

        }

    }
}
