using Spectre.Console;

namespace Atm.Presentation.Console;

public class ScenarioRunner
{
    private IEnumerable<IScenarioProvider> _providers;

    public ScenarioRunner(IEnumerable<IScenarioProvider> providers)
    {
        _providers = providers;
    }

    public void Run()
    {
        while (_providers.Any())
        {
            IEnumerable<IScenario> scenarios = GetScenarios();

            SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
                .Title("Select action")
                .AddChoices(scenarios)
                .UseConverter(x => x.Name);

            IScenario scenario = AnsiConsole.Prompt(selector);
            _providers = scenario.Run();
        }
    }

    private IEnumerable<IScenario> GetScenarios()
    {
        foreach (IScenarioProvider provider in _providers)
        {
            if (provider.TryGetScenario(out IScenario? scenario))
                yield return scenario;
        }
    }
}