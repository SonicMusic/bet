using FluentValidation;
using Serilog;

namespace Bet.API;

public static class DependecyInjection
{
    public static IServiceCollection AddAPI(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSerilog();
        services.AddValidatorsFromAssembly(typeof(DependecyInjection).Assembly);

        return services;
    }
}