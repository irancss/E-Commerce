using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {

        #region Get 
        Task<IEnumerable<Product>> GetProductsAsync();
        IEnumerable<Product> GetProducts();


        Task<Product> GetProductByIdAsync(string id);
        Product GetProductById(string id);



        Task<IEnumerable<Product>> GetProductByNameAsync(string name);
        IEnumerable<Product> GetProductByName(string name);


        Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName);
        IEnumerable<Product> GetProductByCategory(string categoryName);

        #endregion


        #region Crud

        Task CreateProduct(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(string id);

        #endregion






    }
}
