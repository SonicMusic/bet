using Bet.Domain.Shared;
using Bet.Domain.Shared.ValueObjects;
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

        builder.OwnsOne(t => t.TournamentList, tlb =>
        {
            tlb.ToJson();

            tlb.OwnsMany(tl => tl.Tournaments, tb =>
            {
                tb.Property(t => t.Name).IsRequired();
                tb.Property(t => t.Country).IsRequired();
            });
        });

        builder.Property(t => t.Logo).HasDefaultValue(false);
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}