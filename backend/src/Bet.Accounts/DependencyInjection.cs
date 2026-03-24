using Bet.Application.Accounts;
using Bet.Application.Accounts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bet.Accounts;

public static class DependencyInjection
{
    public static IServiceCollection AddAccounts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AccountDbContext>();
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.JWT));
        services.AddTransient<ITokenProvider, JwtTokenProvider>();
        services.RegisterIdentity();

        return services;
    }
    
    private static void RegisterIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<User, Role>(options => { options.User.RequireUniqueEmail = true; })
            .AddEntityFrameworkStores<AccountDbContext>()
            .AddDefaultTokenProviders();
    }
}