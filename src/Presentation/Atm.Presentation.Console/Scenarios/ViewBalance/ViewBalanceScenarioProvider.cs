using System.Diagnostics.CodeAnalysis;
using Atm.Application.Contracts.Users;

namespace Atm.Presentation.Console.Scenarios.ViewBalance;

public class ViewBalanceScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;

    public ViewBalanceScenarioProvider(IUserService userService)
    {
        _userService = userService;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new ViewBalanceScenario(_userService);
        return true;
    }
}