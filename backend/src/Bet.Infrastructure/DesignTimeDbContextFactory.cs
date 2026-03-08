using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Bet.Infrastructure;

/// <summary>
/// Used by EF Core design-time tools (e.g. migrations) to create ApplicationDbContext.
/// Set ConnectionStrings__Database in env or ensure appsettings.json is available from startup project.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__Database")
            ?? "Host=localhost;Database=bet;Username=postgres;Password=postgres";

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:Database"] = connectionString
            })
            .Build();

        return new ApplicationDbContext(configuration);
    }
}
