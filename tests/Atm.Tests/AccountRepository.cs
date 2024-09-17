using System.Collections.Generic;
using System.Linq;
using Atm.Application.Abstractions.Repositories;
using Atm.Application.Models.Accounts;

namespace Atm.Tests;

public class AccountRepository : IAccountRepository
{
    private readonly List<Account> _accounts;

    public AccountRepository(Account account)
    {
        _accounts = new List<Account>() { account };
    }

    public IEnumerable<Account> Accounts => _accounts;

    public bool CreateNewAccount(long id, int pin, int money)
    {
        _accounts.Add(new Account(id, pin, money));
        return true;
    }

    public bool UpdateAccount(long id, int money)
    {
        for (int i = 0; i < _accounts.Count; i++)
        {
            if (_accounts[i].Id == id)
            {
                _accounts[i] = new Account(_accounts[i].Id, _accounts[i].Pin, money);
            }
        }

        return true;
    }

    public Account? FindAccount(long id)
    {
        return _accounts.FirstOrDefault(t => t.Id == id);
    }
}