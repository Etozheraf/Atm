using Atm.Application.Extensions;
using Atm.Infrastructure.DataAccess.Extensions;
using Atm.Presentation.Console;
using Atm.Presentation.Console.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

var collection = new ServiceCollection();

collection
    .AddApplication()
    .AddInfrastructureDataAccess(configuration =>
    {
        configuration.Host = Environment.GetEnvironmentVariable("Host");
        configuration.Port = Int32.Parse(Environment.GetEnvironmentVariable("Port"));
        configuration.Username = Environment.GetEnvironmentVariable("Username");
        configuration.Password = Environment.GetEnvironmentVariable("Password");
        configuration.Database = Environment.GetEnvironmentVariable("Database");
        configuration.SslMode = Environment.GetEnvironmentVariable("Prefer");
    })
    .AddPresentationConsole();

ServiceProvider provider = collection.BuildServiceProvider();
using IServiceScope scope = provider.CreateScope();

scope.UseInfrastructureDataAccess();

IScenarioRunnerProvider scenarioRunnerProvider = scope.ServiceProvider
.GetRequiredService<DefaultScenarioRunnerProvider>();

while (true)
{
    ScenarioRunner scenarioRunner = scenarioRunnerProvider.Create();

    scenarioRunner.Run();
    AnsiConsole.Ask<string>("Press any key");
    AnsiConsole.Clear();
}
