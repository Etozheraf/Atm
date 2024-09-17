namespace Atm.Application.Models.Operations;

public record Operation(long Id, OperationState OperationState, long UserId, int Sum);