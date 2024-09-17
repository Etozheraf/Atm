using Atm.Application.Contracts.Admins;
using Atm.Application.Models.Operations;
using Spectre.Console;

namespace Atm.Presentation.Console.Scenarios.ViewHistory;

public class ViewHistoryScenario : IScenario
{
    private readonly IAdminService _adminService;

    public ViewHistoryScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "View history";
    public IEnumerable<IScenarioProvider> Run()
    {
        IEnumerable<Operation> operations = _adminService.ViewHistory();

        AnsiConsole.WriteLine("History");

        var table = new Table();
        table.AddColumn("Operation State");
        table.AddColumn("User id");
        table.AddColumn("Sum");

        foreach (Operation operation in operations)
        {
            string operationState = operation.OperationState switch
            {
                OperationState.Completed => "Completed",
                OperationState.Rejected => "Rejected",
                _ => string.Empty,
            };

            table.AddRow($"{operationState}", $"{operation.UserId}", $"{operation.Sum}");
        }

        AnsiConsole.Write(table);
        return new List<IScenarioProvider>();
    }
}