using Atm.Application.Contracts.Users;
using Atm.Application.Models.Accounts;
using Atm.Application.Users;
using Xunit;

namespace Atm.Tests;

public class WithdrawalTest
{
    [Theory]
    [InlineData(1000, -1000, 0)]
    [InlineData(1123, -1000, 123)]
    [InlineData(1000, -1, 999)]
    public void CorrectWithdrawalTest(int balance, int sum, int result)
    {
        var accountRepository = new AccountRepository(new Account(1, 1111, balance));
        var userService = new UserService(accountRepository);

        userService.Login(1, 1111);
        userService.ChangeBalance(sum);
        Account? account = accountRepository.FindAccount(1);

        if (account is null)
        {
            Assert.Fail();
        }

        Assert.Equal(result, account.Money);
    }

    [Theory]
    [InlineData(0, -1000)]
    [InlineData(1000, -1123)]
    [InlineData(1000, -1001)]
    public void WrongWithdrawalTest(int balance, int sum)
    {
        var accountRepository = new AccountRepository(new Account(1, 1111, balance));
        var userService = new UserService(accountRepository);

        userService.Login(1, 1111);
        ChangeBalanceResult result = userService.ChangeBalance(sum);

        Assert.IsType<ChangeBalanceResult.NotEnoughMoney>(result);
    }
}