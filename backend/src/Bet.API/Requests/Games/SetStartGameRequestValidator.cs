using FluentValidation;

namespace Bet.API.Requests.Games;

public class SetStartGameRequestValidator : AbstractValidator<SetStartGameRequest>
{
    public SetStartGameRequestValidator()
    {
        RuleFor(g => g.DateTimeOffset)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateTimeOffset.Now);
    }
}