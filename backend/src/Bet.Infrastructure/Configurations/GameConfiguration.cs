using Bet.Domain.GameManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations;

public class GameConfiguration: IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("games");

        builder.HasKey(g => g.Id);

        // Связь: HomeTeam (1) -> (много) Game
        builder.HasOne(g => g.HomeTeam)
            .WithMany(t => t.HomeGames)
            .HasForeignKey(g => g.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        // Связь: AwayTeam (1) -> (много) Game
        builder.HasOne(g => g.AwayTeam)
            .WithMany(t => t.AwayGames)
            .HasForeignKey(g => g.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}