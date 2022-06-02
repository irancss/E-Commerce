using ECommerce.Aggregator.Models;

namespace ECommerce.Aggregator.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<CatalogViewModel>> GetCatalogAsync()
    {
       await _httpClient.get
    }

    public async Task<IEnumerable<CatalogViewModel>> GetCatalogByCategoryAsync(string categoryName)
    {
        throw new NotImplementedException();
    }

    public async Task<CatalogViewModel> GetCatalogByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}