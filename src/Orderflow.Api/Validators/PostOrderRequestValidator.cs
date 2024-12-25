using FluentValidation;
using Orderflow.Api.Routes.Order.Models;
using Orderflow.Domain.Models.Enums;

namespace Orderflow.Validators;

public class PostOrderRequestValidator : AbstractValidator<PostOrderRequest>
{
    public PostOrderRequestValidator()
    {
        RuleFor(x => x.InstrumentId)
            .NotEmpty()
            .NotNull()
            .WithMessage("InstrumentId required")
            .Must(BeAValidGuid)
            .WithMessage("InstrumentId invalid");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .NotNull()
            .WithMessage("Quantity required")
            .Must(BeAValidQuantity)
            .WithMessage("Quantity invalid");

        RuleFor(x => x.Type)
            .NotNull()
            .WithMessage("Type required")
            .Must(i => Enum.IsDefined(typeof(OrderType), i))
            .WithMessage("Type invalid");
    }

    private bool BeAValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }

    private bool BeAValidQuantity(int quantity)
    {
        return quantity > 0;
    }
}