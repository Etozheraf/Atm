using System.Diagnostics.CodeAnalysis;
using Atm.Application.Contracts.Admins;

namespace Atm.Presentation.Console.Scenarios.ViewHistory;

public class ViewHistoryScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;

    public ViewHistoryScenarioProvider(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new ViewHistoryScenario(_adminService);
        return true;
    }
}