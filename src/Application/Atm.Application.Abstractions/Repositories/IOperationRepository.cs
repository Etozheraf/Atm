using Atm.Application.Models.Operations;

namespace Atm.Application.Abstractions.Repositories;

public interface IOperationRepository
{
    IEnumerable<Operation> GetAll();
    void AddOperation(OperationState operationState, long userId, int sum);
}