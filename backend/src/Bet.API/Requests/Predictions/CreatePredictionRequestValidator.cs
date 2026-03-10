using FluentValidation;

namespace Bet.API.Requests.Predictions;

public class CreatePredictionRequestValidator : AbstractValidator<CreatePredictionRequest>
{
    public CreatePredictionRequestValidator()
    {
        RuleFor(p => p.HomeTeamGoals)
            .NotNull().GreaterThanOrEqualTo(0);
        RuleFor(p => p.AwayTeamGoals)
            .NotNull().GreaterThanOrEqualTo(0);
    }
}