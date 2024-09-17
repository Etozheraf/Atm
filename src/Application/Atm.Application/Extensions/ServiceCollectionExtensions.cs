using Atm.Application.Admins;
using Atm.Application.Contracts.Admins;
using Atm.Application.Contracts.Users;
using Atm.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Atm.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<UserService>();
        collection.AddScoped<IUserService, LoggingUserService>();
        collection.AddScoped<IAdminService, AdminService>();

        return collection;
    }
}