using Bet.Contracts.Requests.Teams;
using Bet.Domain.Shared;
using FluentValidation;

namespace Bet.API.Validators.Team;

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