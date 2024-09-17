using Atm.Application.Abstractions.Repositories;
using Atm.Application.Contracts.Admins;
using Atm.Application.Contracts.Users;
using Atm.Application.Models.Operations;
using Lab5.Application.Models.Admins;

namespace Atm.Application.Admins;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IOperationRepository _operationRepository;
    private long _currentUserId;

    public AdminService(IAdminRepository adminRepository, IAccountRepository accountRepository, IOperationRepository operationRepository)
    {
        _adminRepository = adminRepository;
        _accountRepository = accountRepository;
        _operationRepository = operationRepository;
        _currentUserId = -1;
    }

    public CreateAccountResult CreateAccount(long id, int pin, int money)
    {
        if (_accountRepository.FindAccount(id) is not null)
        {
            return new CreateAccountResult.RepeatingId();
        }

        bool result = _accountRepository.CreateNewAccount(id, pin, money);

        if (result)
        {
            return new CreateAccountResult.Success();
        }

        return new CreateAccountResult.Fault();
    }

    public CreateAccountResult CreateAccount(long id, int pin, string password)
    {
        if (_adminRepository.FindAccount(id) is not null)
        {
            return new CreateAccountResult.RepeatingId();
        }

        bool result = _adminRepository.CreateNewAccount(id, pin, password);

        if (result)
        {
            return new CreateAccountResult.Success();
        }

        return new CreateAccountResult.Fault();
    }

    public LoginResult Login(long id, int pin, string password)
    {
        Admin? admin = _adminRepository.FindAccount(id);

        if (admin is null)
        {
            return new LoginResult.WrongId();
        }

        if (admin.Pin != pin)
        {
            return new LoginResult.WrongPin();
        }

        if (admin.Password != password)
        {
            return new LoginResult.WrongPassword();
        }

        _currentUserId = id;
        return new LoginResult.Success();
    }

    public bool UpdatePassword(string password)
    {
        Admin? admin = _adminRepository.FindAccount(_currentUserId);

        return admin is null && _adminRepository.UpdatePassword(_currentUserId, password);
    }

    public IEnumerable<Operation> ViewHistory()
    {
        return _operationRepository.GetAll();
    }
}