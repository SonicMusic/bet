using Bet.Contracts.Requests.Games;
using Bet.Domain.Shared;
using FluentValidation;

namespace Bet.API.Validators.Game;

public class CreateGameRequestValidator : AbstractValidator<CreateGameRequest>
{
    public CreateGameRequestValidator()
    {
        RuleFor(g => g.HomeTeam)
            .NotEmpty()
            .MaximumLength(Constants.Team.MAX_NAME_LENGTH)
            .MinimumLength(Constants.Team.SHORT_NAME_LENGTH);

        RuleFor(g => g.AwayTeam)
            .NotEmpty()
            .MaximumLength(Constants.Team.MAX_NAME_LENGTH)
            .MinimumLength(Constants.Team.SHORT_NAME_LENGTH)
            .NotEqual(g => g.HomeTeam);
    }
}