using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }


        //[HttpGet(Name = "GetProducts")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<IEnumerable<Product>> GetProducts()
        //{
        //    return Ok(_productRepository.GetProducts());
        //}

       [HttpGet("{id:length(24)}", Name = "GetProductAsync")]
       [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
       [ProducesResponseType(StatusCodes.Status404NotFound)]
       public async Task<ActionResult<Product>> GetProductByIdAsync(string id)
       {
           var product = await _productRepository.GetProductByIdAsync(id);

           if (product != null) return Ok(product);

           _logger.LogError($"product with id : {id} , not found");
           return NotFound();
       }

    //    [HttpGet("{id:length(24)}", Name = "GetProduct")]
    //    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    //    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //    public ActionResult<Product> GetProductById(string id)
    //    {
    //        var product = _productRepository.GetProductById(id);

    //        if (product != null) return Ok(product);

    //        _logger.LogError($"product with id : {id} , not found");
    //        return NotFound();
    //    }


        [Route("/[action]/{category}", Name = "GetProductByCategoryAsync")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategoryAsync(string category)
        {
            var products = await _productRepository.GetProductByCategoryAsync(category);
            return Ok(products);
        }

    //    //[Route("/action/{category}", Name = "GetProductByCategory")]
    //    [HttpGet]
    //    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
    //    public ActionResult<IEnumerable<Product>> GetProductByCategory(string category)
    //    {
    //        var products = _productRepository.GetProductByCategory(category);
    //        return Ok(products);
    //    }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        public async Task<ActionResult<Product>> CreateProductAsync([FromBody] Product product)
        {
            try
            {
                await _productRepository.CreateProduct(product);
                return CreatedAtRoute("GetProductByIdAsync", new { id = product.Id }, product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        public async Task<IActionResult> UpdateProductAsync(Product product)
        {
            return Ok(await _productRepository.UpdateProductAsync(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProductAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        public async Task<IActionResult> DeleteProductByIdAsync(string id)
        {
            return Ok(await _productRepository.DeleteProductAsync(id));
        }
    }
}
