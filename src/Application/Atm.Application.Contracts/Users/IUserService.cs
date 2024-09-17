namespace Atm.Application.Contracts.Users;

public interface IUserService
{
    LoginResult Login(long id, int pin);
    ViewBalanceResult ViewBalance();
    ChangeBalanceResult ChangeBalance(int sum);
}