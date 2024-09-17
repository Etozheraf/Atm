using Atm.Application.Contracts.Users;
using Atm.Presentation.Console.Scenarios.Replenishment;
using Atm.Presentation.Console.Scenarios.ViewBalance;
using Atm.Presentation.Console.Scenarios.Withdrawal;
using Spectre.Console;

namespace Atm.Presentation.Console.Scenarios.Login;

public class LoginAsUserScenario : IScenario
{
    private readonly IUserService _userService;

    public LoginAsUserScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Login as user";

    public IEnumerable<IScenarioProvider> Run()
    {
        long id = AnsiConsole.Ask<long>("Enter your id");

        int pin = AnsiConsole.Ask<int>("Enter your pin");

        LoginResult result = _userService.Login(id, pin);

        string message = result switch
        {
            LoginResult.Success => "Successful login",
            LoginResult.WrongId => "User not found",
            LoginResult.WrongPin => "Invalid pin code",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };
        AnsiConsole.WriteLine(message);

        if (result is LoginResult.Success)
        {
            return new List<IScenarioProvider>
            {
                new ViewBalanceScenarioProvider(_userService),
                new ReplenishmentScenarioProvider(_userService),
                new WithdrawalScenarioProvider(_userService),
            };
        }

        return new List<IScenarioProvider>();
    }
}