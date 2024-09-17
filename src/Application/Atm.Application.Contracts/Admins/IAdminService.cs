using Atm.Application.Contracts.Users;
using Atm.Application.Models.Operations;

namespace Atm.Application.Contracts.Admins;

public interface IAdminService
{
    CreateAccountResult CreateAccount(long id, int pin, int money);
    CreateAccountResult CreateAccount(long id, int pin, string password);
    LoginResult Login(long id, int pin, string password);

    bool UpdatePassword(string password);
    IEnumerable<Operation> ViewHistory();
}