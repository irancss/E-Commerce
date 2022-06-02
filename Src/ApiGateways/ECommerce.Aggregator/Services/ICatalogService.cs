using ECommerce.Aggregator.Models;

namespace ECommerce.Aggregator.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogViewModel>> GetCatalogAsync();

        Task<IEnumerable<CatalogViewModel>> GetCatalogByCategoryAsync(string categoryName);

        Task<CatalogViewModel> GetCatalogByIdAsync(string id);
    }
}
