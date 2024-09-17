using Atm.Application.Abstractions.Repositories;
using Atm.Application.Models.Accounts;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Atm.Infrastructure.DataAccess.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AccountRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public Account? FindAccount(long id)
    {
        const string sql = """
        SELECT account_id, pin, money
        FROM accounts
        WHERE account_id = :id;
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

        return new Account(
            Id: reader.GetInt64(0),
            Pin: reader.GetInt32(1),
            Money: reader.GetInt32(2));
    }

    public bool CreateNewAccount(long id, int pin, int money)
    {
        const string sql = """
        INSERT INTO accounts(account_id ,pin, money)
        VALUES(:id, :pin, :money);
        """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        command.AddParameter("pin", pin);
        command.AddParameter("money", money);

        int result = command.ExecuteNonQuery();

        return result != -1;
    }

    public bool UpdateAccount(long id, int money)
    {
        const string sql = """
        UPDATE accounts
        SET money = :money
        WHERE account_id = :id ;
        """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        command.AddParameter("money", money);

        int result = command.ExecuteNonQuery();

        return result != -1;
    }
}