using Bet.Domain.GameManagement;
using Bet.Domain.GameManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations.Write;

public class PredictionConfiguration : IEntityTypeConfiguration<Prediction>
{
    public void Configure(EntityTypeBuilder<Prediction> builder)
    {
        builder.ToTable("predictions");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedNever(); // Id генерируется доменом

        builder.Property(p => p.HomeTeamPredictedGoals).IsRequired().HasDefaultValue(0);
        builder.Property(p => p.AwayTeamPredictedGoals).IsRequired().HasDefaultValue(0);

        builder.Property(p => p.Status)
            .HasConversion<string>()
            .HasDefaultValue(StatusPrediction.Waiting);
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}