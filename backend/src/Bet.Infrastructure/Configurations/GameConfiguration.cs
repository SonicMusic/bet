using Bet.Domain.GameManagement;
using Bet.Domain.TeamManagement;
using Bet.Domain.TournamentManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("games");

        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).ValueGeneratedNever(); // Id генерируется доменом

        builder.Property(g => g.TournamentId).IsRequired();
        builder.Property(g => g.HomeTeamId).IsRequired();
        builder.Property(g => g.AwayTeamId).IsRequired();

        // Game → Tournament (многие к одному)
        builder.HasOne<Tournament>()
            .WithMany()
            .HasForeignKey(g => g.TournamentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(g => g.TournamentId);

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

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
        
        builder.Property(g => g.HomeTeamGoals).IsRequired().HasDefaultValue(0);
        builder.Property(g => g.AwayTeamGoals).IsRequired().HasDefaultValue(0);

        builder.Property(g => g.Status)
            .HasConversion<string>()
            .HasDefaultValue(StatusGame.NotStarted);

        builder.Property(g => g.Start)
            .IsRequired();
    }
}