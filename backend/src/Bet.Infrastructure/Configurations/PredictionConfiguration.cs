using Bet.Domain.GameManagement;
using Bet.Domain.PredictionManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations;

public class PredictionConfiguration : IEntityTypeConfiguration<Prediction>
{
    public void Configure(EntityTypeBuilder<Prediction> builder)
    {
        builder.ToTable("predictions");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever(); // Id генерируется доменом

        builder.HasOne<Game>()
            .WithMany()
            .HasForeignKey(p => p.GameId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(p => p.GameId);

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");

        builder.Property(p => p.HomeTeamGoals).IsRequired().HasDefaultValue(0);
        builder.Property(p => p.AwayTeamGoals).IsRequired().HasDefaultValue(0);

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasDefaultValue(StatusPrediction.Pending);
    }
}