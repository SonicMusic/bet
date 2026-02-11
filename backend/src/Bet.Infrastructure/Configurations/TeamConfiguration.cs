using Bet.Domain.GameManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations;

public class TeamConfiguration: IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("teams");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired();

       
    }
}