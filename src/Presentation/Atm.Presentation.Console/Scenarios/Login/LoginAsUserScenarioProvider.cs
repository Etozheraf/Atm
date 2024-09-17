using System.Diagnostics.CodeAnalysis;
using Atm.Application.Contracts.Users;

namespace Atm.Presentation.Console.Scenarios.Login;

public class LoginAsUserScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;

    public LoginAsUserScenarioProvider(IUserService service)
    {
        _service = service;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        scenario = new LoginAsUserScenario(_service);
        return true;
    }
}