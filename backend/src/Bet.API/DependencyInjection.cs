using FluentValidation;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Bet.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSerilog();
        services.AddSwaggerGen();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        return services;
    }
    
    
}