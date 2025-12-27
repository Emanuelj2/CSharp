using UsersApp.Models;

namespace UsersApp.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(); //since there could be many products, we return an IEnumerable
        Task<IEnumerable<Product>> GetProductByCategoryAsync(ProductCategory category); //this is a ProductCategory enum
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(int id, Product product);
        Task<bool> DeleteProductAsync(int id);

    }
}
