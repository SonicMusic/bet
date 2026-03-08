using Bet.Domain.TeamManagement;
using Bet.Domain.TournamentManagement;
using Bet.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bet.Infrastructure.Configurations;

public class TournamentTeamConfiguration : IEntityTypeConfiguration<TournamentTeam>
{
    public void Configure(EntityTypeBuilder<TournamentTeam> builder)
    {
        builder.ToTable("tournament_teams");

        builder.HasKey(tt => new { tt.TournamentId, tt.TeamId });

        builder.HasOne<Tournament>()
            .WithMany()
            .HasForeignKey(tt => tt.TournamentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Team>()
            .WithMany()
            .HasForeignKey(tt => tt.TeamId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
