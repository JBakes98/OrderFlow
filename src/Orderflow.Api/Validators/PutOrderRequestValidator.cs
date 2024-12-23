using FluentValidation;
using Orderflow.Api.Routes.Order.Models;

namespace Orderflow.Validators;

public class PutOrderRequestValidator : AbstractValidator<PutOrderRequest>
{
    public PutOrderRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithErrorCode("Id required")
            .Must(BeAValidGuid)
            .WithMessage("Id invalid");

        RuleFor(x => x.Status)
            .NotEmpty()
            .NotNull()
            .WithMessage("Status required")
            .IsInEnum()
            .WithMessage("Status invalid");
    }

    private bool BeAValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}