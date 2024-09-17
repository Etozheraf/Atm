using System.Diagnostics.CodeAnalysis;
using Atm.Application.Contracts.Users;

namespace Atm.Presentation.Console.Scenarios.Withdrawal;

public class WithdrawalScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;

    public WithdrawalScenarioProvider(IUserService userService)
    {
        _userService = userService;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new WithdrawalScenario(_userService);
        return true;
    }
}