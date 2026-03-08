using Bet.Contracts.Requests.Predictions;
using FluentValidation;

namespace Bet.API.Validators.Prediction;

public class CreatePredictionRequestValidator : AbstractValidator<CreatePredictionRequest>
{
    public CreatePredictionRequestValidator()
    {
        RuleFor(p => p.HomeTeamGoals).NotNull().GreaterThan(0);
        RuleFor(p => p.AwayTeamGoals).NotNull().GreaterThan(0);
    }
}