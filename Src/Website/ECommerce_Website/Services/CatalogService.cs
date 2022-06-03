using ECommerce_Website.Extensions;
using ECommerce_Website.Models;

namespace ECommerce_Website.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<CatalogModel>> GetAllCatalog()
    {
        var response = await _httpClient.GetAsync("/catalog");
        return await response.ReadContentAs<List<CatalogModel>>();
    }

    public async Task<CatalogModel> GetCatalogById(string id)
    {
        var response = await _httpClient.GetAsync($"/catalog/{id}");
        return await response.ReadContentAs<CatalogModel>();
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
    {
        var response = await _httpClient.GetAsync($"/catalog/GetProductByCategory/{category}");
        return await response.ReadContentAs<IEnumerable<CatalogModel>>();
    }

    public async Task<CatalogModel> CreateCatalog(CatalogModel model)
    {
        var response = await _httpClient.PostAsJson($"/Catalog", model);

        if (response.IsSuccessStatusCode)
        {
            return await response.ReadContentAs<CatalogModel>();
        }

        throw new Exception("Something went wrong when calling api");
    }

}