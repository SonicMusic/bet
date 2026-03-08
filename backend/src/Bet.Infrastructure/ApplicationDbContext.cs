using Bet.Domain.GameManagement;
using Bet.Domain.PredictionManagement;
using Bet.Domain.TeamManagement;
using Bet.Domain.TournamentManagement;
using Bet.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Bet.Infrastructure;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    private const string DATABASE = "Database";
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Tournament> Tournaments => Set<Tournament>();
    public DbSet<TournamentTeam> TournamentTeams => Set<TournamentTeam>();
    public DbSet<Prediction> Predictions => Set<Prediction>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            configuration.GetConnectionString(DATABASE));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => builder.AddConsole());
}