using FluentValidation;
using Orderflow.Api.Routes.Order.Models;
using Orderflow.Domain.Models.Enums;

namespace Orderflow.Validators;

public class PutOrderRequestValidator : AbstractValidator<PutOrderRequest>
{
    public PutOrderRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithErrorCode("Id required")
            .Must(BeAValidGuid)
            .WithMessage("Id invalid");

        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("Status required");

        When(x => !string.IsNullOrEmpty(x.Status), () =>
        {
            RuleFor(x => x.Status)
                .Must(i => Enum.IsDefined(typeof(OrderStatus), i))
                .WithMessage("Status invalid");
        });
    }

    private bool BeAValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}