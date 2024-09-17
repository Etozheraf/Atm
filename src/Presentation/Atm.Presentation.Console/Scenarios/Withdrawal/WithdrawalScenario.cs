using Atm.Application.Contracts.Users;
using Spectre.Console;

namespace Atm.Presentation.Console.Scenarios.Withdrawal;

public class WithdrawalScenario : IScenario
{
    private readonly IUserService _userService;

    public WithdrawalScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Withdarawal";
    public IEnumerable<IScenarioProvider> Run()
    {
        int sum = AnsiConsole.Ask<int>("Enter sum");

        ChangeBalanceResult result = _userService.ChangeBalance(-sum);

        string message = result switch
        {
            ChangeBalanceResult.Success => "Successful replenishment",
            ChangeBalanceResult.NotEnoughMoney => "Not enough money",
            ChangeBalanceResult.WrongId => "Error",
            ChangeBalanceResult.Fault => "Error",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };
        AnsiConsole.WriteLine(message);

        return new List<IScenarioProvider>();
    }
}