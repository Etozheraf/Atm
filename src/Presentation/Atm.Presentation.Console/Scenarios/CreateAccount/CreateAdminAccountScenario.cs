using Atm.Application.Contracts.Admins;
using Spectre.Console;

namespace Atm.Presentation.Console.Scenarios.CreateAccount;

public class CreateAdminAccountScenario : IScenario
{
    private readonly IAdminService _adminService;

    public CreateAdminAccountScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Create new admin account";
    public IEnumerable<IScenarioProvider> Run()
    {
        long id = AnsiConsole.Ask<long>("Enter id");

        int pin = AnsiConsole.Ask<int>("Enter pin");

        string password = AnsiConsole.Ask<string>("Enter password");

        CreateAccountResult result = _adminService.CreateAccount(id, pin, password);

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