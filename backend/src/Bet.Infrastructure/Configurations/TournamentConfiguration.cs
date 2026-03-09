using Bet.Domain.Shared;
using Bet.Domain.TeamManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations;

public class TournamentConfiguration: IEntityTypeConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        builder.ToTable("tournament");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(Constants.General.MAX_NAME_LENGTH)
            .IsRequired();
        
        builder.Property(t => t.Nation)
            .HasMaxLength(Constants.General.MAX_NAME_LENGTH)
            .IsRequired();
    }
}