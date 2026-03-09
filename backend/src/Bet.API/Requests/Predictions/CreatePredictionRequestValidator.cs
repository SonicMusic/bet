using FluentValidation;

namespace Bet.API.Requests.Predictions;

public class CreatePredictionRequestValidator : AbstractValidator<CreatePredictionRequest>
{
    public CreatePredictionRequestValidator()
    {
        RuleFor(p => p.HomeTeamGoals).NotNull().LessThan(0);
        RuleFor(p => p.AwayTeamGoals).NotNull().LessThan(0);
    }
}