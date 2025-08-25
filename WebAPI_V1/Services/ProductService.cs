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
        public List<Product> GetAll(
    string sortColumn = "Code",
    bool ascending = true,
    string? searchColumn = null,
    string? searchText = null)
        {
            var products = new List<Product>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string Map(string col) => (col ?? "").ToLower() switch
                {
                    "id" => "Id",
                    "code" => "Code",
                    "name" => "Name",
                    "barcode" => "Barcode",
                    "quantity" => "Quantity",
                    "group" => "[Group]",  
                    "type" => "[Type]",
                    "taxrate" => "TaxRate",
                    "price" => "Price",
                    _ => "Code"     
                };

                string orderCol = Map(sortColumn);
                string direction = ascending ? "ASC" : "DESC";
                string columns = "Id, Code, Name, Barcode, Quantity, [Group], [Type], TaxRate, Price";

                string sql = $"SELECT {columns} FROM Products";

                bool hasSearch = !string.IsNullOrWhiteSpace(searchText) && !string.IsNullOrWhiteSpace(searchColumn);
                if (hasSearch)
                {
                    string whereCol = Map(searchColumn);
                    sql += $" WHERE CAST({whereCol} AS NVARCHAR(255)) LIKE @searchTerm";
                }

                sql += $" ORDER BY {orderCol} {direction}";

                using (var command = new SqlCommand(sql, connection))
                {
                    if (hasSearch)
                    {
                        command.Parameters.AddWithValue("@searchTerm", "%" + searchText + "%");
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = (int)reader["Id"],
                                Code = reader["Code"].ToString(),
                                Name = reader["Name"].ToString(),
                                Barcode = reader["Barcode"] == DBNull.Value ? null : reader["Barcode"].ToString(),
                                Quantity = reader["Quantity"] == DBNull.Value ? null : (int?)reader["Quantity"],
                                Group = reader["Group"] == DBNull.Value ? null : reader["Group"].ToString(),
                                Type = reader["Type"] == DBNull.Value ? null : reader["Type"].ToString(),
                                TaxRate = reader["TaxRate"] == DBNull.Value ? null : (int?)reader["TaxRate"],
                                Price = reader["Price"] == DBNull.Value ? null : Convert.ToDecimal(reader["Price"]),
                            });
                        }
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
                            Quantity = (int)reader["Quantity"],
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
                SqlCommand command = new SqlCommand(
                    "UPDATE Products SET Code = @Code, Name = @Name, Barcode = @Barcode, Quantity = @Quantity, [Group] = @Group, [Type] = @Type, TaxRate = @TaxRate, Price = @Price WHERE Id = @Id",
                    connection
                );
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Code", product.Code ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Name", product.Name ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Barcode", (object?)product.Barcode ?? DBNull.Value);
                command.Parameters.AddWithValue("@Quantity", (object?)product.Quantity ?? DBNull.Value);
                command.Parameters.AddWithValue("@Group", (object?)product.Group ?? DBNull.Value);
                command.Parameters.AddWithValue("@Type", (object?)product.Type ?? DBNull.Value);
                command.Parameters.AddWithValue("@TaxRate", (object?)product.TaxRate ?? DBNull.Value);
                command.Parameters.AddWithValue("@Price", (object?)product.Price ?? DBNull.Value);
                int affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }

        public bool Create(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO Products (Code, Name, Barcode, Quantity, [Group], [Type], TaxRate, Price)
                                VALUES (@Code, @Name, @Barcode, @Quantity, @Group, @Type, @TaxRate, @Price);";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Code", product.Code ?? throw new ArgumentNullException(nameof(product.Code)));
                command.Parameters.AddWithValue("@Name", product.Name ?? throw new ArgumentNullException(nameof(product.Name)));
                command.Parameters.AddWithValue("@Barcode", product.Barcode);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
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