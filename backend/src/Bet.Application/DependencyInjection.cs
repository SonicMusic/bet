using Bet.Application.Games.Create;
using Bet.Application.Games.Delete;
using Bet.Application.Games.Update;
using Bet.Application.Predictions.Create;
using Bet.Application.Teams;
using Bet.Application.Teams.Create;
using Bet.Application.Teams.Update;
using Bet.Application.Tournaments;
using Microsoft.Extensions.DependencyInjection;

namespace Bet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddScoped<CreateTeamHandler>();
        services.AddScoped<UpdateTeamNameHandler>();
        services.AddScoped<UploadTeamLogoHandler>();
        services.AddScoped<GetTeamByNameHandler>();

        services.AddScoped<CreateGameHandler>();
        services.AddScoped<UpdateGameHandler>();
        services.AddScoped<DeleteGameHandler>();
        
        services.AddScoped<CreatePredictionHandler>();
        // services.AddScoped<UpdateStatusPredictionHandler>();
        // services.AddScoped<DeletePredictionHandler>();

        services.AddScoped<CreateTournamentHandler>();


        return services;
    }
}