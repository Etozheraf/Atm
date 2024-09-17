using Atm.Application.Contracts.Admins;
using Atm.Application.Contracts.Users;
using Atm.Presentation.Console.Scenarios.ChangePassword;
using Atm.Presentation.Console.Scenarios.CreateAccount;
using Atm.Presentation.Console.Scenarios.ViewHistory;
using Spectre.Console;

namespace Atm.Presentation.Console.Scenarios.Login;

public class LoginAsAdminScenario : IScenario
{
    private readonly IAdminService _adminService;
    public LoginAsAdminScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Login as admin";
    public IEnumerable<IScenarioProvider> Run()
    {
        long id = AnsiConsole.Ask<long>("Enter your id");

        int pin = AnsiConsole.Ask<int>("Enter your pin");

        string password = AnsiConsole.Ask<string>("Enter your password");

        LoginResult result = _adminService.Login(id, pin, password);

        string message = result switch
        {
            LoginResult.Success => "Successful login",
            LoginResult.WrongId => "User not found",
            LoginResult.WrongPin => "Invalid pin code",
            LoginResult.WrongPassword => "Invalid password",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };
        AnsiConsole.WriteLine(message);

        if (result is LoginResult.Success)
        {
            return new List<IScenarioProvider>
            {
                new ChangePasswordScenarioProvider(_adminService),
                new CreateAdminAccountScenarioProvider(_adminService),
                new CreateUserAccountScenarioProvider(_adminService),
                new ViewHistoryScenarioProvider(_adminService),
            };
        }

        return new List<IScenarioProvider>();
    }
}