using Company_API_Jhonnier.Interfaces;
using Company_API_Jhonnier.Models;
using Npgsql;

namespace Company_API_Jhonnier.Repositories;
public class UserRepository : IUserRepository
{
    private readonly string _connectionString;
    public UserRepository()
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    public async Task<int> AddUser(AddUser user)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var query = "INSERT INTO Users (Name, Email, CreatedAt) VALUES (@Name, @Email, NOW())";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", user.Nombre);
            command.Parameters.AddWithValue("@Email", user.Email);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public async Task<int> AddUserInRoles(int UserId, int RolId)
    {
        try
        {
            if (await ValidarUserInRoles(UserId, RolId) > 0)
                return 0;

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var query = "INSERT INTO UsersInRoles (UserId, RolId) VALUES (@UserId, @RolId)";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", UserId);
            command.Parameters.AddWithValue("@RolId", RolId);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public async Task<int> ValidarUserInRoles(int UserId, int RolId)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = $"select count(*) from UsersInRoles where UserId = {UserId} and RolId ={RolId}";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            int count = 0;
            while (await reader.ReadAsync())
            {
                count = reader.GetInt32(0);
            }
            return count;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<int> ValidarRoles(List<string> rol)
    {
        try
        {
            string roles = string.Empty;
            foreach (var item in rol)
                roles += $"'{item}',";

            roles = roles.Substring(0, roles.Length - 1);
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = $"select count(*) from Roles where Rolname in ({roles}) ";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            int count = 0;
            while (await reader.ReadAsync())
            {
                count = reader.GetInt32(0);
            }
            return count;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public async Task<int> ValidateUser(string Nombre, string Email, int idUser = 0)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = $"select count(*) from Users where (name ='{Nombre}' or email = '{Email}') and userId != {idUser}";
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
    public async Task<int> UpdateUser(UpdateUser user)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var query = "Update Users set Name = @Name, Email = @Email where userId = @UserId ";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", user.Nombre);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@UserId", user.IdUser);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
}
