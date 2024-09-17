namespace Atm.Application.Contracts.Users;

public abstract record ChangeBalanceResult
{
    private ChangeBalanceResult() { }

    public sealed record Success : ChangeBalanceResult;

    public sealed record Fault : ChangeBalanceResult;

    public sealed record WrongId : ChangeBalanceResult;

    public sealed record NotEnoughMoney : ChangeBalanceResult;
}