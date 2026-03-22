using Bet.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations.Read;

public class GameDtoConfiguration : IEntityTypeConfiguration<GameDto>
{
    public void Configure(EntityTypeBuilder<GameDto> builder)
    {
        builder.ToTable("games");

        builder.HasKey(g => g.GameId);

        builder.HasMany(g => g.Predictions)
            .WithOne()
            .HasForeignKey("game_id");
    }
}