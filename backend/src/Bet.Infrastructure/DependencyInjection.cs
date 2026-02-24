using Bet.Application;
using Bet.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Bet.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<ITeamsRepository, TeamsRepository>();
        services.AddScoped<IGamesRepository, GamesRepository>();

        return services;
    }
}