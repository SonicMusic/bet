using Bet.Contracts.Requests.Teams;
using Bet.Domain.Shared;
using FluentValidation;

namespace Bet.API.Validators.Team;

public class UpdateTeamNameRequestValidator : AbstractValidator<UpdateTeamNameRequest>
{
    public UpdateTeamNameRequestValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .MaximumLength(Constants.General.MAX_NAME_LENGTH)
            .MinimumLength(Constants.General.MIN_NAME_LENGTH);
    }
}