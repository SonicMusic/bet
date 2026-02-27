using Bet.Application.Games;
using Bet.Application.Teams;
using Microsoft.Extensions.DependencyInjection;

namespace Bet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddScoped<CreateTeamHandler>();
        services.AddScoped<UpdateTeamHandler>();
        services.AddScoped<DeleteTeamHandler>();

        services.AddScoped<CreateGameHandler>();
        services.AddScoped<UpdateGameHandler>();
        services.AddScoped<DeleteGameHandler>();


        return services;
    }
}