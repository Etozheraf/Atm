using Atm.Application.Models.Accounts;

namespace Atm.Application.Abstractions.Repositories;

public interface IAccountRepository
{
    bool CreateNewAccount(long id, int pin, int money);
    bool UpdateAccount(long id, int money);
    Account? FindAccount(long id);
}