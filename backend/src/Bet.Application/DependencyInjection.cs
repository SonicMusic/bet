using Bet.Application.Teams;
using Microsoft.Extensions.DependencyInjection;

namespace Bet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddScoped<CreateTeamHandler>();

        return services;
    }
}