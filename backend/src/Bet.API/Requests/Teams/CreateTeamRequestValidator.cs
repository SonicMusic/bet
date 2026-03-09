using Bet.Domain.Shared;
using FluentValidation;

namespace Bet.API.Requests.Teams;

public class CreateTeamRequestValidator : AbstractValidator<CreateTeamRequest>
{
    public CreateTeamRequestValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(Constants.General.MAX_NAME_LENGTH)
            .MinimumLength(Constants.General.MIN_NAME_LENGTH);
    }
}