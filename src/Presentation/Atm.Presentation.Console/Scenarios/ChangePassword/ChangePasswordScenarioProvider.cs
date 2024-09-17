using System.Diagnostics.CodeAnalysis;
using Atm.Application.Contracts.Admins;

namespace Atm.Presentation.Console.Scenarios.ChangePassword;

public class ChangePasswordScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;

    public ChangePasswordScenarioProvider(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new ChangePasswordScenario(_adminService);
        return true;
    }
}