using Bet.Application.Teams.CreateTeam;
using Bet.Application.Teams.Delete;
using Bet.Application.Teams.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Bet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddScoped<CreateTeamHandler>();
        services.AddScoped<UpdateTeamHandler>();
        services.AddScoped<DeleteTeamHandler>();

        return services;
    }
}