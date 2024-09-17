using Atm.Application.Models.Accounts;
using Atm.Application.Users;
using Xunit;

namespace Atm.Tests;

public class ReplenishmentTest
{
    [Theory]
    [InlineData(0, 1000, 1000)]
    [InlineData(1000, 1123, 2123)]
    [InlineData(1000, 1, 1001)]
    public void CorrectReplenishmentTest(int balance, int sum, int result)
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
}