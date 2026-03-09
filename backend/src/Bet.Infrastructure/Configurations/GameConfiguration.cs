using Bet.Domain.GameManagement;
using Bet.Domain.Shared;
using Bet.Domain.TeamManagement;
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


        builder.Property(g => g.HomeTeamGoals).IsRequired().HasDefaultValue(0);
        builder.Property(g => g.AwayTeamGoals).IsRequired().HasDefaultValue(0);

        builder.OwnsOne(g => g.Tournament, tb =>
        {
            tb.ToJson("tournament");

            tb.Property(t => t.Name).IsRequired().HasMaxLength(Constants.General.MAX_NAME_LENGTH);
            tb.Property(t => t.Country).IsRequired().HasMaxLength(Constants.General.MAX_NAME_LENGTH);
        });

        builder.Property(g => g.Status)
            .HasConversion<string>()
            .HasDefaultValue(StatusGame.NotStarted);

        builder.Property(g => g.Start)
            .IsRequired();

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}