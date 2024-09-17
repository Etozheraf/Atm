using System.Diagnostics.CodeAnalysis;
using Atm.Application.Contracts.Users;

namespace Atm.Presentation.Console.Scenarios.Replenishment;

public class ReplenishmentScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;

    public ReplenishmentScenarioProvider(IUserService userService)
    {
        _userService = userService;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new ReplenishmentScenario(_userService);
        return true;
    }
}