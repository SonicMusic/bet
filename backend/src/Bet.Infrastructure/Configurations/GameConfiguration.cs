using Bet.Domain.GameManagement;
using Bet.Domain.TeamManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations;

public class GameConfiguration: IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("games");

        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id)
            .ValueGeneratedNever(); // Id генерируется доменом

        builder.Property(g => g.HomeTeamId).IsRequired();
        builder.Property(g => g.AwayTeamId).IsRequired();
        
        // FK на Team без навигационных свойств
        builder.HasOne<Team>()
            .WithMany()
            .HasForeignKey(g => g.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Team>()
            .WithMany()
            .HasForeignKey(g => g.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(g => g.HomeTeamId);
        builder.HasIndex(g => g.AwayTeamId);

    }
}