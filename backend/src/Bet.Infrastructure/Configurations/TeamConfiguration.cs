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
            .HasMaxLength(Constants.General.MAX_LOW_TEXT_LENGTH)
            .IsRequired();
        
        builder.Property(t => t.ShortName)
            .HasMaxLength(Constants.General.MAX_LOW_TEXT_LENGTH)
            .IsRequired(false);
        
        builder.Property(t => t.Country)
            .HasMaxLength(Constants.General.MAX_LOW_TEXT_LENGTH)
            .IsRequired(false);
        
        builder.Property(t => t.Logo)
            .HasMaxLength(Constants.General.MAX_LOW_TEXT_LENGTH)
            .IsRequired(false);
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}