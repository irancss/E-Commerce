using ECommerce_Website.Models;

namespace ECommerce_Website.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetAllCatalog();
        Task<CatalogModel> GetCatalogById(int id);
        Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
        Task<CatalogModel> CreateCatalog(CatalogModel model);
    }
} 
