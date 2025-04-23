using System.Net.Http.Json;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace EShopService.IntegrationTests.Controllers
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_Then_GetById_Should_Return_Same_Product()
        {
            // Arrange
            var newProduct = new Product { Name = "TestProduct", Price = 10.99M };

            // Act
            var postResponse = await _client.PostAsJsonAsync("/api/Product", newProduct);
            postResponse.EnsureSuccessStatusCode();
            var created = await postResponse.Content.ReadFromJsonAsync<Product>();

            var getResponse = await _client.GetAsync($"/api/Product/{created.Id}");
            var fetched = await getResponse.Content.ReadFromJsonAsync<Product>();

            // Assert
            Assert.Equal(created.Id, fetched.Id);
            Assert.Equal("TestProduct", fetched.Name);
        }

        [Fact]
        public async Task Put_Should_Update_Product()
        {
            var product = new Product { Name = "Original", Price = 5M };
            var post = await _client.PostAsJsonAsync("/api/Product", product);
            var created = await post.Content.ReadFromJsonAsync<Product>();

            created.Name = "Updated";

            var put = await _client.PutAsJsonAsync($"/api/Product/{created.Id}", created);
            put.EnsureSuccessStatusCode();

            var get = await _client.GetFromJsonAsync<Product>($"/api/Product/{created.Id}");
            Assert.Equal("Updated", get.Name);
        }

        [Fact]
        public async Task Delete_Should_Remove_Product()
        {
            var product = new Product { Name = "ToDelete", Price = 15M };
            var post = await _client.PostAsJsonAsync("/api/Product", product);
            var created = await post.Content.ReadFromJsonAsync<Product>();

            var delete = await _client.DeleteAsync($"/api/Product/{created.Id}");
            Assert.Equal(System.Net.HttpStatusCode.NoContent, delete.StatusCode);

            var get = await _client.GetAsync($"/api/Product/{created.Id}");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, get.StatusCode);
        }

        [Fact]
        public async Task Get_Should_Return_All()
        {
            var response = await _client.GetFromJsonAsync<IEnumerable<Product>>("/api/Product");
            Assert.NotNull(response);
        }
    }
}
