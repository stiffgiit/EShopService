using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Repositories;

namespace EShop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository _productRepository;

        
        public ProductService(IRepository productRepository)
        {
            _productRepository = productRepository;
        }

        
        public async Task<Product> AddProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentException("Product cannot be null");

            // Zapisz produkt w repozytorium
            return await _productRepository.AddAsync(product);
        }

        // Pobieranie wszystkich produktów
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        // Pobieranie produktu po ID
        public async Task<Product> GetProductByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid product ID");

            return await _productRepository.GetByIdAsync(id);
        }

        // Aktualizowanie produktu
        public async Task<Product> UpdateProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentException("Product cannot be null");

            return await _productRepository.UpdateAsync(product);
        }

        // Usuwanie produktu
        public async Task<bool> DeleteProductAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid product ID");

            return await _productRepository.DeleteAsync(id);
        }

    }
}
