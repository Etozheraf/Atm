using System.Diagnostics.CodeAnalysis;

namespace Atm.Presentation.Console;

public interface IScenarioProvider
{
    bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}