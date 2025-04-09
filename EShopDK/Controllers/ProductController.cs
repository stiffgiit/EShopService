using EShop.Application.Services;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productService.GetAllProductsAsync();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            return product;
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Product cannot be null");

            var createdProduct = await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, [FromBody] Product product)
        {
            if (product == null || product.Id != id)
                return BadRequest("Product ID mismatch");

            var updatedProduct = await _productService.UpdateProductAsync(product);
            if (updatedProduct == null)
                return NotFound();

            return updatedProduct;
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
