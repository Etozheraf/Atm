namespace Atm.Application.Contracts.Admins;

public abstract record CreateAccountResult
{
    private CreateAccountResult() { }
    public sealed record Success : CreateAccountResult;

    public sealed record Fault : CreateAccountResult;
    public sealed record RepeatingId : CreateAccountResult;
}