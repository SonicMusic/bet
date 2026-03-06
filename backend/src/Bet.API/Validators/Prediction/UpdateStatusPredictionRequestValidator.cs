using Bet.Contracts.Requests.Predictions;
using Bet.Domain.Shared;
using FluentValidation;

namespace Bet.API.Validators.Prediction;

public class UpdateStatusPredictionRequestValidator : AbstractValidator<UpdateStatusPredictionRequest>
{
    public UpdateStatusPredictionRequestValidator()
    {
        RuleFor(p => p.Status)
            .NotEmpty()
            .MinimumLength(Constants.Team.MIN_NAME_LENGTH)
            .MaximumLength(Constants.Team.MAX_NAME_LENGTH);
    }
}