using Lab5.Application.Models.Admins;

namespace Atm.Application.Abstractions.Repositories;

public interface IAdminRepository
{
    bool CreateNewAccount(long id, int pin, string password);

    bool UpdatePassword(long id, string password);
    Admin? FindAccount(long id);
}