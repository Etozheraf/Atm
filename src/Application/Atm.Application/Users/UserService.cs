using Atm.Application.Abstractions.Repositories;
using Atm.Application.Contracts.Users;
using Atm.Application.Models.Accounts;

namespace Atm.Application.Users;

public class UserService : IUserService
{
    private readonly IAccountRepository _repository;
    private long _currentUserId;

    public UserService(IAccountRepository repository)
    {
        _repository = repository;
        _currentUserId = -1;
    }

    public LoginResult Login(long id, int pin)
    {
        Account? account = _repository.FindAccount(id);

        if (account is null)
        {
            return new LoginResult.WrongId();
        }

        if (account.Pin != pin)
        {
            return new LoginResult.WrongPin();
        }

        _currentUserId = account.Id;
        return new LoginResult.Success();
    }

    public ViewBalanceResult ViewBalance()
    {
        Account? account = _repository.FindAccount(_currentUserId);

        if (account is null)
        {
            return new ViewBalanceResult.WrongId();
        }

        return new ViewBalanceResult.Success(account.Money);
    }

    public ChangeBalanceResult ChangeBalance(int sum)
    {
        Account? account = _repository.FindAccount(_currentUserId);

        if (account is null)
        {
            return new ChangeBalanceResult.WrongId();
        }

        if (account.Money < -1 * sum)
        {
            return new ChangeBalanceResult.NotEnoughMoney();
        }

        bool success = _repository.UpdateAccount(account.Id, account.Money + sum);

        if (success)
        {
            return new ChangeBalanceResult.Success();
        }

        return new ChangeBalanceResult.Fault();
    }
}