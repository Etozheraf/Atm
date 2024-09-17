using Atm.Presentation.Console.Scenarios.Login;

namespace Atm.Presentation.Console;

public class DefaultScenarioRunnerProvider : IScenarioRunnerProvider
{
    private readonly LoginAsAdminScenarioProvider _loginAsAdmin;
    private readonly LoginAsUserScenarioProvider _loginAsUser;

    public DefaultScenarioRunnerProvider(LoginAsAdminScenarioProvider loginAsAdmin, LoginAsUserScenarioProvider loginAsUser)
    {
        _loginAsAdmin = loginAsAdmin;
        _loginAsUser = loginAsUser;
    }

    public ScenarioRunner Create()
    {
        return new ScenarioRunner(new List<IScenarioProvider>
        {
            _loginAsUser,
            _loginAsAdmin,
        });
    }
}