using Bet.Domain.Shared;
using Bet.Domain.TeamManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations;

public class TeamConfiguration: IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("teams");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .ValueGeneratedNever(); // Id генерируется доменом

        builder.Property(t => t.Name)
            .HasMaxLength(Constants.Team.MAX_NAME_LENGTH)
            .IsRequired();
        builder.HasIndex(t => t.Name).IsUnique();
        
        builder.Property(t => t.ShortName)
            .HasMaxLength(Constants.Team.SHORT_NAME_LENGTH)
            .IsRequired(false);
        
        builder.Property(t => t.Country)
            .HasMaxLength(Constants.Team.MAX_NAME_LENGTH)
            .IsRequired(false);
        
        builder.Property(t => t.Logo).HasDefaultValue(false);
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}