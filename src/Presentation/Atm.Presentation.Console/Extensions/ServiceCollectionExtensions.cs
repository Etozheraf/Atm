using Atm.Presentation.Console.Scenarios.Login;
using Microsoft.Extensions.DependencyInjection;

namespace Atm.Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<LoginAsAdminScenarioProvider>();
        collection.AddScoped<LoginAsUserScenarioProvider>();
        collection.AddScoped<DefaultScenarioRunnerProvider>();

        return collection;
    }
}