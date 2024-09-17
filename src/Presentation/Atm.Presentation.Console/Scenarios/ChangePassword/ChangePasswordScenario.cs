using Atm.Application.Contracts.Admins;
using Spectre.Console;

namespace Atm.Presentation.Console.Scenarios.ChangePassword;

public class ChangePasswordScenario : IScenario
{
    private readonly IAdminService _adminService;

    public ChangePasswordScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Change password";
    public IEnumerable<IScenarioProvider> Run()
    {
        string password = AnsiConsole.Ask<string>("Enter password");

        bool result = _adminService.UpdatePassword(password);

        string message = result switch
        {
            true => "Successful change password",
            false => "Error",
        };
        AnsiConsole.WriteLine(message);

        return new List<IScenarioProvider>();
    }
}