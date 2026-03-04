using Bet.Application.Games;
using Bet.Application.Predictions;
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
        services.AddScoped<UploadLogoTeamHandler>();

        services.AddScoped<CreateGameHandler>();
        services.AddScoped<UpdateGameHandler>();
        services.AddScoped<DeleteGameHandler>();
        
        services.AddScoped<CreatePredictionHandler>();
        services.AddScoped<UpdateStatusPredictionHandler>();
        services.AddScoped<DeletePredictionHandler>();


        return services;
    }
}