using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Stok_Takip.Models;

namespace Stok_Takip.Services
{
    internal class StockServices_APIVersion
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7039/api/Products/";

        private static readonly string[] AllowedColumns = new[]
        {
            "Id","Code","Name","Barcode","Quantity","Group","Type","TaxRate","Price"
        };

        public StockServices_APIVersion()
        {
            _httpClient = new HttpClient();
        }

        private static string NormalizeColumn(string? col, string fallback = "Code")
        {
            if (string.IsNullOrWhiteSpace(col)) return fallback;
            var match = AllowedColumns.FirstOrDefault(c => c.Equals(col, StringComparison.OrdinalIgnoreCase));
            return match ?? fallback;
        }

        private string BuildQuery(string sortColumn, bool ascending, string? searchColumn, string? searchText)
        {
            var qp = new List<string>
            {
                $"sortColumn={Uri.EscapeDataString(NormalizeColumn(sortColumn))}",
                $"ascending={(ascending ? "true" : "false")}",
            };

            if (!string.IsNullOrWhiteSpace(searchText) && !string.IsNullOrWhiteSpace(searchColumn))
            {
                var sc = NormalizeColumn(searchColumn);
                qp.Add($"searchColumn={Uri.EscapeDataString(sc)}");
                qp.Add($"searchText={Uri.EscapeDataString(searchText)}");
            }

            return string.Join("&", qp);
        }

        public async Task<List<Product>> GetAllProductsAsync(
            string sortColumn = "Code",
            bool ascending = true,
            string? searchColumn = null,
            string? searchText = null)
        {
            var queryString = BuildQuery(sortColumn, ascending, searchColumn, searchText);
            var url = _apiBaseUrl + "GetAll?" + queryString;

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return new List<Product>();

            return await response.Content.ReadFromJsonAsync<List<Product>>() ?? new List<Product>();
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(_apiBaseUrl + "Delete?id=" + id);
            return response.IsSuccessStatusCode;
        }

        public async Task<Product?> UpdateProductAsync(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync(
                    _apiBaseUrl + $"Update?id={product.Id}",
                    product
                );
            if (response.IsSuccessStatusCode)
            {
                if ((int)response.StatusCode == 204) return product;
                return await response.Content.ReadFromJsonAsync<Product>();
            }

            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Error (Update): {error}");
            return null;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl + "Create", product);
            return response.IsSuccessStatusCode;
        }

    }
}
