using Atm.Application.Abstractions.Repositories;
using Atm.Application.Contracts.Users;
using Atm.Application.Models.Operations;

namespace Atm.Application.Users;

public class LoggingUserService : IUserService
{
    private readonly IOperationRepository _repository;
    private readonly UserService _service;
    private long _currentUserId;

    public LoggingUserService(IOperationRepository repository, UserService service)
    {
        _repository = repository;
        _service = service;
        _currentUserId = -1;
    }

    public LoginResult Login(long id, int pin)
    {
        _currentUserId = id;
        return _service.Login(id, pin);
    }

    public ViewBalanceResult ViewBalance()
    {
        ViewBalanceResult result = _service.ViewBalance();

        if (result is ViewBalanceResult.Success)
        {
            _repository.AddOperation(
                OperationState.Completed,
                _currentUserId,
                0);
            return result;
        }

        _repository.AddOperation(
            OperationState.Rejected,
            _currentUserId,
            0);
        return result;
    }

    public ChangeBalanceResult ChangeBalance(int sum)
    {
        ChangeBalanceResult result = _service.ChangeBalance(sum);

        if (result is ChangeBalanceResult.Success)
        {
            _repository.AddOperation(
                OperationState.Completed,
                _currentUserId,
                sum);
            return result;
        }

        _repository.AddOperation(
            OperationState.Rejected,
            _currentUserId,
            sum);
        return result;
    }
}