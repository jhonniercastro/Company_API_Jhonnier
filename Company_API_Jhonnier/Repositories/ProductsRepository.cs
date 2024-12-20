using Company_API_Jhonnier.Interfaces;
using Company_API_Jhonnier.Models;
using Npgsql;

namespace Company_API_Jhonnier.Repositories;
public class ProductsRepository : IProductsRepository
{
    private readonly string _connectionString;
    public ProductsRepository()
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    public async Task<int> AddProduct(AddProduct product)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var query = "INSERT INTO Products (Name, inventory, CreatedAt) VALUES (@Name, @inventory, NOW())";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", product.name);
            command.Parameters.AddWithValue("@inventory", product.inventory);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public async Task<int> ValidateProduct(string Nombre, int idProduct = 0)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = $"select count(*) from Products where name ='{Nombre}' and productId != {idProduct}";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            int UserCount = 0;
            while (await reader.ReadAsync())
            {
                UserCount = reader.GetInt32(0);
            }
            return UserCount;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public async Task<int> UpdateProduct(UpdateProduct product)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var query = "Update Products set Name = @Name, inventory = @inventory where productId = @productId ";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", product.name);
            command.Parameters.AddWithValue("@inventory", product.inventory);
            command.Parameters.AddWithValue("@productId", product.idProduct);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public async Task<int> DeleteProduct(int IdProduct)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = "Update Products set isdeleted = true where productId = @productId ";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@productId", IdProduct);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

}
