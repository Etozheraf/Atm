using System.Diagnostics.CodeAnalysis;
using Atm.Application.Contracts.Admins;

namespace Atm.Presentation.Console.Scenarios.CreateAccount;

public class CreateUserAccountScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    public CreateUserAccountScenarioProvider(IAdminService service)
    {
        _service = service;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new CreateUserAccountScenario(_service);
        return true;
    }
}