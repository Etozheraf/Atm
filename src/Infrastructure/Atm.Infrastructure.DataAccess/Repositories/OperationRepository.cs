using Atm.Application.Abstractions.Repositories;
using Atm.Application.Models.Operations;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace Atm.Infrastructure.DataAccess.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public OperationRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public void AddOperation(OperationState operationState, long userId, int sum)
    {
        const string sql = """
        INSERT INTO operations
        VALUES(default, :operationState, :userId, :sum);
        """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("operationState", operationState); // command.AddParameter("operationState", operationState);
        command.AddParameter("userId", userId);
        command.AddParameter("sum", sum);

        command.ExecuteNonQuery();
    }

    public IEnumerable<Operation> GetAll()
    {
        const string sql = """
        select *
        from operations
        """;

        NpgsqlConnection connection = _connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        using NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new Operation(
                Id: reader.GetInt64(0),
                OperationState: reader.GetFieldValue<OperationState>(1),
                UserId: reader.GetInt64(2),
                Sum: reader.GetInt32(3));
        }
    }
}