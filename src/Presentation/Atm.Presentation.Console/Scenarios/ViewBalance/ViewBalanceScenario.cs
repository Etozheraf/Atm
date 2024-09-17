using Atm.Application.Contracts.Users;
using Spectre.Console;

namespace Atm.Presentation.Console.Scenarios.ViewBalance;

public class ViewBalanceScenario : IScenario
{
    private readonly IUserService _userService;

    public ViewBalanceScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "View balance";
    public IEnumerable<IScenarioProvider> Run()
    {
        ViewBalanceResult result = _userService.ViewBalance();

        string message = result switch
        {
            ViewBalanceResult.Success success => $"Your balance: {success.Balance}",
            ViewBalanceResult.WrongId => "Error",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);

        return new List<IScenarioProvider>();
    }
}