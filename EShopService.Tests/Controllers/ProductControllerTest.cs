using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EShop.Application.Services;
using EShop.Domain.Models;
using EShopService.Controllers;

namespace EShopService.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }

        [Fact]
        public async Task Get_ReturnsAllProducts()
        {
            _mockService.Setup(s => s.GetAllProductsAsync())
                        .ReturnsAsync(new List<Product> { new Product { Id = 1, Name = "Test" } });

            var result = await _controller.Get();

            Assert.Single(result);
        }

        [Fact]
        public async Task GetById_ProductExists_ReturnsProduct()
        {
            var product = new Product { Id = 1, Name = "Test" };
            _mockService.Setup(s => s.GetProductByIdAsync(1))
                        .ReturnsAsync(product);

            var result = await _controller.Get(1);
            var okResult = Assert.IsType<ActionResult<Product>>(result);
            Assert.Equal(product.Id, okResult.Value.Id);
        }

        [Fact]
        public async Task GetById_ProductNotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.GetProductByIdAsync(1))
                        .ReturnsAsync((Product)null);

            var result = await _controller.Get(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Post_ValidProduct_ReturnsCreatedProduct()
        {
            var newProduct = new Product { Id = 1, Name = "Test" };
            _mockService.Setup(s => s.AddProductAsync(newProduct))
                        .ReturnsAsync(newProduct);

            var result = await _controller.Post(newProduct);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(newProduct.Id, ((Product)createdAtActionResult.Value).Id);
        }

        [Fact]
        public async Task Post_NullProduct_ReturnsBadRequest()
        {
            var result = await _controller.Post(null);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Put_ValidProduct_ReturnsUpdatedProduct()
        {
            var product = new Product { Id = 1, Name = "Updated" };
            _mockService.Setup(s => s.UpdateProductAsync(product))
                        .ReturnsAsync(product);

            var result = await _controller.Put(1, product);

            var okResult = Assert.IsType<ActionResult<Product>>(result);
            Assert.Equal(product.Id, okResult.Value.Id);
        }

        [Fact]
        public async Task Put_IdMismatch_ReturnsBadRequest()
        {
            var result = await _controller.Put(1, new Product { Id = 2 });

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Delete_ProductExists_ReturnsNoContent()
        {
            _mockService.Setup(s => s.DeleteProductAsync(1))
                        .ReturnsAsync(true);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ProductNotFound_ReturnsNotFound()
        {
            _mockService.Setup(s => s.DeleteProductAsync(1))
                        .ReturnsAsync(false);

            var result = await _controller.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
