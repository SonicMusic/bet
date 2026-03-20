using FluentValidation;

namespace Bet.API.Requests.Games;

public class ChangeStatusGameRequestValidator : AbstractValidator<ChangeStatusGameRequest>
{
    public ChangeStatusGameRequestValidator()
    {
        RuleFor(g => g.Status).NotEmpty();
    }
}