using Atm.Application.Abstractions.Repositories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Lab5.Application.Models.Admins;
using Npgsql;

namespace Atm.Infrastructure.DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AdminRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public bool CreateNewAccount(long id, int pin, string password)
    {
        const string sql = """
        INSERT INTO admins(admin_id, pin, password)
        VALUES(:id, :pin, :password);
        """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        command.AddParameter("pin", pin);
        command.AddParameter("password", password);

        int result = command.ExecuteNonQuery();

        return result != -1;
    }

    public bool UpdatePassword(long id, string password)
    {
        const string sql = """
        UPDATE admins
        SET password = :password
        WHERE admin_id = :id ;
        """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        command.AddParameter("password", password);

        int result = command.ExecuteNonQuery();

        return result != -1;
    }

    public Admin? FindAccount(long id)
    {
        const string sql = """
        SELECT admin_id, pin, password
        FROM admins
        WHERE admin_id = :id;
        """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new Admin(
            Id: reader.GetInt64(0),
            Pin: reader.GetInt32(1),
            Password: reader.GetString(2));
    }
}