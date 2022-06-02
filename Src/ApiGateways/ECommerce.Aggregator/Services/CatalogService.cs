using ECommerce.Aggregator.Extensions;
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
        var response = await _httpClient.GetAsync("/api/v1/Catalog");
        return await response.ReadContentAs<List<CatalogViewModel>>();
    }

    public async Task<IEnumerable<CatalogViewModel>> GetCatalogByCategoryAsync(string categoryName)
    {
        var response = await _httpClient.GetAsync($"/api/v1/Catalog/GetProductByCategoryAsync/{categoryName}");
        return await response.ReadContentAs<List<CatalogViewModel>>();
    }

    public async Task<CatalogViewModel> GetCatalogByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"/api/v1/Catalog/{id}");
        return await response.ReadContentAs<CatalogViewModel>();
    }
}