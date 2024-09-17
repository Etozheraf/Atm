namespace Atm.Application.Contracts.Users;

public abstract record ViewBalanceResult
{
    private ViewBalanceResult() { }

    public sealed record Success(int Balance) : ViewBalanceResult;

    public sealed record WrongId : ViewBalanceResult;
}