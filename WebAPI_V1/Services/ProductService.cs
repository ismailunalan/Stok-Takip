using Microsoft.Data.SqlClient;
using WebAPI_V1.Models;

namespace WebAPI_V1.Services
{
    public class ProductService
    {
        private readonly string _connectionString;

        public ProductService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Tüm ürünleri getirir
        public List<Product> GetAll()
        {
            var products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Products", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = (int)reader["product_id"],
                            Code = reader["stock_code"].ToString(),
                            Name = reader["stock_name"].ToString(),
                            Barcode = reader["barcode"].ToString(),
                            ShelfNo = (int)reader["shelf_no"],
                            Group = reader["stock_group"].ToString(),
                            Type = reader["stock_type"].ToString(),
                            Tax = (int)reader["tax_rate"],
                            Price = (int)reader["price"]
                        });
                    }
                }
            }
            return products;
        }
        
        public Product Get(int id) {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE product_id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            Id = (int)reader["product_id"],
                            Code = reader["stock_code"].ToString(),
                            Name = reader["stock_name"].ToString(),
                            Barcode = reader["barcode"].ToString(),
                            ShelfNo = (int)reader["shelf_no"],
                            Group = reader["stock_group"].ToString(),
                            Type = reader["stock_type"].ToString(),
                            Tax = (int)reader["tax_rate"],
                            Price = (int)reader["price"]
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DELETE FROM Products WHERE product_id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }

        public bool Update(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Products SET stock_code = @Code, stock_name = @Name, barcode = @Barcode, shelf_no = @ShelfNo, stock_group = @Group, stock_type = @Type, tax_rate = @Tax, price = @Price WHERE product_id = @Id", connection);
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Code", product.Code);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Barcode", product.Barcode);
                command.Parameters.AddWithValue("@ShelfNo", product.ShelfNo);
                command.Parameters.AddWithValue("@Group", product.Group);
                command.Parameters.AddWithValue("@Type", product.Type);
                command.Parameters.AddWithValue("@Tax", product.Tax);
                command.Parameters.AddWithValue("@Price", product.Price);
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }
    }
}