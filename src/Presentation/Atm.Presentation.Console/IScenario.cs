namespace Atm.Presentation.Console;

public interface IScenario
{
    string Name { get; }

    IEnumerable<IScenarioProvider> Run();
}