using Bet.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations.Read;

public class PredictionDtoConfiguration : IEntityTypeConfiguration<PredictionDto>
{
    public void Configure(EntityTypeBuilder<PredictionDto> builder)
    {
        builder.ToTable("predictions");

        builder.HasKey(p => p.Id);
        
    }
}