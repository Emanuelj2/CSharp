using Microsoft.EntityFrameworkCore;
using UsersApp.Data;
using UsersApp.Models;
using UsersApp.Models.ViewModels;

namespace UsersApp.Service
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            bool exist = await _context.Products.AnyAsync(p => p.Name == p.Name && p.Category == product.Category);

            if(exist)
            {   
                throw new Exception("Product with the same name already exists.");

            }
           
            _context.Products.Add(product);
            await  _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(ProductCategory category)
        {
            return await _context.Products
                                 .Where(p => p.Category == category)
                                 .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }
            return product;
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.Category = product.Category;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
}
