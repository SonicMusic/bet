using Bet.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations.Read;

public class TeamDtoConfiguration: IEntityTypeConfiguration<TeamDto>
{
    public void Configure(EntityTypeBuilder<TeamDto> builder)
    {
        builder.ToTable("teams");

        builder.HasKey(t => t.Id);
        
    }
}