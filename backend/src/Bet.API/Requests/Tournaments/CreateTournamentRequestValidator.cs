using Bet.Domain.Shared;
using FluentValidation;

namespace Bet.API.Requests.Tournaments;

public class CreateTournamentRequestValidator : AbstractValidator<CreateTournamentRequest>
{
    public CreateTournamentRequestValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .MaximumLength(Constants.General.MAX_NAME_LENGTH);
        RuleFor(t => t.Nation)
            .NotEmpty()
            .MaximumLength(Constants.General.MAX_NAME_LENGTH);
    }
}