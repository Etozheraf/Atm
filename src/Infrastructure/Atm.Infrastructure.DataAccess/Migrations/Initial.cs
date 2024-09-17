using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Atm.Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
    """
    CREATE TYPE operation_state AS ENUM
        (
            'completed',
            'rejected'
            );

    CREATE TABLE Accounts
    (
        account_id BIGINT PRIMARY KEY,
        pin INT NOT NULL ,
        money INT NOT NULL
    );

    CREATE TABLE Admins
    (
        admin_id BIGINT PRIMARY KEY,
        pin INT NOT NULL,
        password TEXT NOT NULL
    );

    CREATE TABLE Operations
    (
        operation_id BIGINT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
        operation_state operation_state NOT NULL,
        user_id BIGINT NOT NULL,
        sum INT
    );
    """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
    """
    DROP TABLE Accounts;
    DROP TABLE Admins;
    DROP TABLE Operations;

    DROP TYPE operation_state;
    """;
}