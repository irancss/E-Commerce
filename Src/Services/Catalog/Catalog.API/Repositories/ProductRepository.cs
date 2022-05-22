using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _catalogContext;

    public ProductRepository(ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext ?? throw new ArgumentException(nameof(_catalogContext));
    }


    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _catalogContext.Products.Find(c => true).ToListAsync();
    }

    public IEnumerable<Product> GetProducts()
    {
        return _catalogContext.Products.Find(c => true).ToList();
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _catalogContext.Products.Find(c => c.Id == id).FirstOrDefaultAsync();
    }

    public Product GetProductById(string id)
    {
        return _catalogContext.Products.Find(c => c.Id == id).FirstOrDefault();
    }

    public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
    {
        var filter = Builders<Product>.Filter.ElemMatch(c => c.Name, name);

        return await _catalogContext.Products.Find(filter).ToListAsync();
    }

    public IEnumerable<Product> GetProductByName(string name)
    {
        var filter = Builders<Product>.Filter.Eq(c => c.Name, name);

        return _catalogContext.Products.Find(filter).ToList();
    }

    public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName)
    {
        var filter = Builders<Product>.Filter.Eq(c => c.Category, categoryName);

        return await _catalogContext.Products.Find(filter).ToListAsync();

    }

    public IEnumerable<Product> GetProductByCategory(string categoryName)
    {
        var filter = Builders<Product>.Filter.Eq(c => c.Category, categoryName);

        return _catalogContext.Products.Find(filter).ToList();
    }

    public async Task CreateProduct(Product product)
    {
        await _catalogContext.Products.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var updateResult =
            await _catalogContext.Products.ReplaceOneAsync(filter: c => c.Id == product.Id, replacement: product);

        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        var filter = Builders<Product>.Filter.Eq(c => c.Id, id);

        var deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
}