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
            .WithMessage("InstrumentId required");

        When(x => !string.IsNullOrEmpty(x.InstrumentId), () =>
        {
            RuleFor(x => x.InstrumentId)
                .Must(BeAValidGuid)
                .WithMessage("InstrumentId invalid");
        });

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity required")
            .Must(BeAValidQuantity)
            .WithMessage("Quantity invalid");

        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("Type required");

        When(x => !string.IsNullOrEmpty(x.Type), () =>
        {
            RuleFor(x => x.Type)
                .Must(i => Enum.IsDefined(typeof(OrderType), i))
                .WithMessage("Type invalid");
        });
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