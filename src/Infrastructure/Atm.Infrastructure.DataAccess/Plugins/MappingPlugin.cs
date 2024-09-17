using Itmo.Dev.Platform.Postgres.Plugins;
using Atm.Application.Models.Operations;
using Npgsql;

namespace Atm.Infrastructure.DataAccess.Plugins;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<OperationState>();
    }
}