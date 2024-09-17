using Atm.Application.Contracts.Admins;
using Spectre.Console;

namespace Atm.Presentation.Console.Scenarios.CreateAccount;

public class CreateUserAccountScenario : IScenario
{
    private readonly IAdminService _adminService;

    public CreateUserAccountScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Create new user account";
    public IEnumerable<IScenarioProvider> Run()
    {
        long id = AnsiConsole.Ask<long>("Enter id");

        int pin = AnsiConsole.Ask<int>("Enter pin");

        int money = AnsiConsole.Ask<int>("Enter money");

        CreateAccountResult result = _adminService.CreateAccount(id, pin, money);

        string message = result switch
        {
            CreateAccountResult.Success => "Successful login",
            CreateAccountResult.RepeatingId => "This user exist",
            CreateAccountResult.Fault => "Data base error",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };
        AnsiConsole.WriteLine(message);

        return new List<IScenarioProvider>();
    }
}