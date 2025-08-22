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
                var command = new SqlCommand("SELECT Id, Code, Name, Barcode, ShelfNo, [Group], [Type], TaxRate, Price FROM Products", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = (int)reader["Id"],
                            Code = reader["Code"].ToString(),
                            Name = reader["Name"].ToString(),
                            Barcode = reader["Barcode"].ToString(),
                            ShelfNo = (int)reader["ShelfNo"],
                            Group = reader["Group"].ToString(),
                            Type = reader["Type"].ToString(),
                            TaxRate = (int)reader["TaxRate"],
                            Price = (int)reader["Price"]
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
                
                SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            Id = (int)reader["Id"],
                            Code = reader["Code"].ToString(),
                            Name = reader["Name"].ToString(),
                            Barcode = reader["Barcode"].ToString(),
                            ShelfNo = (int)reader["ShelfNo"],
                            Group = reader["Group"].ToString(),
                            Type = reader["Type"].ToString(),
                            TaxRate = (int)reader["TaxRate"],
                            Price = (int)reader["Price"]
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

                SqlCommand command = new SqlCommand("DELETE FROM Products WHERE Id = @Id", connection);
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
                SqlCommand command = new SqlCommand("UPDATE Products SET Code = @Code, Name = @Name, Barcode = @Barcode, ShelfNo = @ShelfNo, Group = @Group, Type = @Type, TaxRate = @TaxRate, Price = @Price WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Code", product.Code);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Barcode", product.Barcode);
                command.Parameters.AddWithValue("@ShelfNo", product.ShelfNo);
                command.Parameters.AddWithValue("@Group", product.Group);
                command.Parameters.AddWithValue("@Type", product.Type);
                command.Parameters.AddWithValue("@TaxRate", product.TaxRate);
                command.Parameters.AddWithValue("@Price", product.Price);
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }

        public bool Create(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO Products (Code, Name, Barcode, ShelfNo, [Group], [Type], TaxRate, Price)
                                VALUES (@Code, @Name, @Barcode, @ShelfNo, @Group, @Type, @TaxRate, @Price);";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Code", product.Code);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Barcode", product.Barcode);
                command.Parameters.AddWithValue("@ShelfNo", product.ShelfNo);
                command.Parameters.AddWithValue("@Group", product.Group);
                command.Parameters.AddWithValue("@Type", product.Type);
                command.Parameters.AddWithValue("@TaxRate", product.TaxRate);
                command.Parameters.AddWithValue("@Price", product.Price);
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }
    }
}