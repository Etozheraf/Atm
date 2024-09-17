namespace Atm.Application.Contracts.Users;

public abstract record LoginResult
{
    private LoginResult() { }

    public sealed record Success : LoginResult;

    public sealed record WrongId : LoginResult;

    public sealed record WrongPin : LoginResult;

    public sealed record WrongPassword : LoginResult;
}