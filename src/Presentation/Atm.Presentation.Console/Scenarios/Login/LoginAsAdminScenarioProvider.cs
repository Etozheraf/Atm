using System.Diagnostics.CodeAnalysis;
using Atm.Application.Contracts.Admins;

namespace Atm.Presentation.Console.Scenarios.Login;

public class LoginAsAdminScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;

    public LoginAsAdminScenarioProvider(IAdminService service)
    {
        _service = service;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new LoginAsAdminScenario(_service);
        return true;
    }
}