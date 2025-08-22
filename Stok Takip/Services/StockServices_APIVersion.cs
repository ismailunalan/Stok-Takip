using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Stok_Takip.Models;

namespace Stok_Takip.Services
{
    internal class StockServices_APIVersion
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:7039/api/Products/";
        public StockServices_APIVersion()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl + "GetAll");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Product>>();
            }
            return new List<Product>();
        }

        public async Task<List<Product>> GetProductAsync(
            string sortColumn = "Code",
            bool ascending = true,
            string? searchColumn = null,
            string? searchText = null)
        {
            var queryParams = new List<string>
            {
                $"sortColumn={sortColumn}",
                $"ascending={ascending.ToString().ToLower()}"
            };
            if (!string.IsNullOrWhiteSpace(searchColumn))
                queryParams.Add($"searchColumn={Uri.EscapeDataString(searchColumn)}");
            if (!string.IsNullOrWhiteSpace(searchText))
                queryParams.Add($"searchText={Uri.EscapeDataString(searchText)}");

            string queryString = string.Join("&", queryParams);
            string url = _apiBaseUrl + "GetAll?" + queryString;

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Product>>();
            }
            return new List<Product>();
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + "Delete?id=" + id);
            return response.IsSuccessStatusCode;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync(_apiBaseUrl + "Update", product);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Product>();
            }
            return null;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl + "Add", product);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Product>();
            }
            return null;
        }
    }
}
