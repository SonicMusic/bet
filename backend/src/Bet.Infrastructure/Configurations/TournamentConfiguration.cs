using Bet.Domain.Shared;
using Bet.Domain.TournamentManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations;

public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        builder.ToTable("tournaments");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedNever();

        builder.Property(t => t.Name)
            .HasMaxLength(Constants.General.MAX_LOW_TEXT_LENGTH)
            .IsRequired();
        builder.HasIndex(t => t.Name).IsUnique();

        builder.Property(t => t.Country)
            .HasMaxLength(Constants.General.MAX_LOW_TEXT_LENGTH)
            .IsRequired(false);

        builder.Property(t => t.Logo).HasDefaultValue(false);

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}
