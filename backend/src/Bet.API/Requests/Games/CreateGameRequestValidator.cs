using Bet.Domain.Shared;
using FluentValidation;

namespace Bet.API.Requests.Games;

public class CreateGameRequestValidator : AbstractValidator<CreateGameRequest>
{
    public CreateGameRequestValidator()
    {
        RuleFor(g => g.HomeTeam)
            .NotEmpty()
            .MaximumLength(Constants.General.MAX_NAME_LENGTH)
            .MinimumLength(Constants.General.MIN_NAME_LENGTH);

        RuleFor(g => g.AwayTeam)
            .NotEmpty()
            .MaximumLength(Constants.General.MAX_NAME_LENGTH)
            .MinimumLength(Constants.General.MIN_NAME_LENGTH)
            .NotEqual(g => g.HomeTeam);
    }
}