using Microsoft.Data.SqlClient;
using Stok_Takip.Models;

namespace Stok_Takip.Services
{
    public class StockService
    {
        private readonly string server;
        public StockService(string sv)
        {
            server = sv;
        }

        public void addProduct(Product p)
        {
            using (SqlConnection connection = new SqlConnection(server))
            {
                connection.Open();

                string query = @"INSERT INTO Products (stock_code, stock_name, barcode, shelf_no, stock_group, stock_type, tax_rate, price) 
                                     VALUES (@code, @name, @barcode, @shelf, @group, @type, @tax, @price);
                                     SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@code", p.StockCode);
                    command.Parameters.AddWithValue("@name", p.StockName);
                    command.Parameters.AddWithValue("@barcode", (object?)p.Barcode ?? DBNull.Value);
                    command.Parameters.AddWithValue("@shelf", (object?)p.ShelfNo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@group", (object?)p.StockGroup ?? DBNull.Value);
                    command.Parameters.AddWithValue("@type", (object?)p.StockType ?? DBNull.Value);
                    command.Parameters.AddWithValue("@tax", (object?)p.TaxRate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@price", (object?)p.Price ?? DBNull.Value);

                    object result = command.ExecuteScalar();
                    p.ProductId = Convert.ToInt32(result);
                    MessageBox.Show("Ürün başarıyla kaydedildi!");
                }
            }
        }

        public void deleteProduct(int productId)
        {
            using (SqlConnection connection = new SqlConnection(server))
            {
                connection.Open();

                string query = "DELETE FROM Products WHERE product_id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", productId);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Ürün başarıyla silindi!");
            }
        }

        public List<Product> getProducts(string sortColumn = "stock_code", bool ascending = true, string? searchColumn = null, string? searchText = null)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(server))
            {
                connection.Open();

                string direction = ascending ? "ASC" : "DESC";
                string query;

                string columns = "product_id, stock_code, stock_name, barcode, shelf_no, stock_group, stock_type, tax_rate, price";

                if (!string.IsNullOrWhiteSpace(searchText) && !string.IsNullOrWhiteSpace(searchColumn))
                {
                    query = $"SELECT {columns} FROM Products WHERE CAST({searchColumn} AS NVARCHAR) LIKE @searchTerm ORDER BY {sortColumn} {direction}";
                }
                else
                {
                    query = $"SELECT {columns} FROM Products ORDER BY {sortColumn} {direction}";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        command.Parameters.AddWithValue("@searchTerm", "%" + searchText + "%");
                    }

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            StockCode = reader["stock_code"].ToString(),
                            StockName = reader["stock_name"].ToString(),
                            Barcode = reader["barcode"] == DBNull.Value ? null : reader["barcode"].ToString(),
                            ShelfNo = reader["shelf_no"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["shelf_no"]),
                            StockGroup = reader["stock_group"] == DBNull.Value ? null : reader["stock_group"].ToString(),
                            StockType = reader["stock_type"] == DBNull.Value ? null : reader["stock_type"].ToString(),
                            TaxRate = reader["tax_rate"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["tax_rate"]),
                            Price = reader["price"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(reader["price"])
                        });
                    }
                    return products;
                }
            }
        }

        public void updateProduct(Product p)
        {
            using (SqlConnection connection = new SqlConnection(server))
            {
                connection.Open();

                string query = @"UPDATE Products
                                 SET stock_code = @code,
                                     stock_name = @name,
                                     barcode = @barcode,
                                     shelf_no = @shelf,
                                     stock_group = @group,
                                     stock_type = @type,
                                     tax_rate = @tax,
                                     price = @price
                                 WHERE product_id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", p.ProductId);
                    command.Parameters.AddWithValue("@code", p.StockCode);
                    command.Parameters.AddWithValue("@name", p.StockName);
                    command.Parameters.AddWithValue("@barcode", (object?)p.Barcode ?? DBNull.Value);
                    command.Parameters.AddWithValue("@shelf", (object?)p.ShelfNo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@group", (object?)p.StockGroup ?? DBNull.Value);
                    command.Parameters.AddWithValue("@type", (object?)p.StockType ?? DBNull.Value);
                    command.Parameters.AddWithValue("@tax", (object?)p.TaxRate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@price", (object?)p.Price ?? DBNull.Value);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Ürün başarıyla güncellendi!");
                }
            }
        }
    }
}