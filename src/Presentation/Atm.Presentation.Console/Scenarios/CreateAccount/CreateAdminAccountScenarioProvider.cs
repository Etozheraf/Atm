using System.Diagnostics.CodeAnalysis;
using Atm.Application.Contracts.Admins;

namespace Atm.Presentation.Console.Scenarios.CreateAccount;

public class CreateAdminAccountScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    public CreateAdminAccountScenarioProvider(IAdminService service)
    {
        _service = service;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new CreateAdminAccountScenario(_service);
        return true;
    }
}