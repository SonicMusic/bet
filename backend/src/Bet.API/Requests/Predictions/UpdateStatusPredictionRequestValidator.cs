using Bet.Domain.Shared;
using FluentValidation;

namespace Bet.API.Requests.Predictions;

public class UpdateStatusPredictionRequestValidator : AbstractValidator<UpdateStatusPredictionRequest>
{
    public UpdateStatusPredictionRequestValidator()
    {
        RuleFor(p => p.Status)
            .NotEmpty()
            .MinimumLength(Constants.General.MIN_NAME_LENGTH)
            .MaximumLength(Constants.General.MAX_NAME_LENGTH);
    }
}