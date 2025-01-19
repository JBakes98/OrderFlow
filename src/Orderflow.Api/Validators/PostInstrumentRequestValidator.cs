using FluentValidation;
using Orderflow.Api.Routes.Instrument.Models;

namespace Orderflow.Validators;

public class PostInstrumentRequestValidator : AbstractValidator<PostInstrumentRequest>
{
    public PostInstrumentRequestValidator()
    {
        RuleFor(x => x.ExchangeId)
            .NotEmpty()
            .WithMessage("ExchangeId required");

        When(x => !string.IsNullOrEmpty(x.ExchangeId), () =>
        {
            RuleFor(x => x.ExchangeId)
                .Must(BeAValidGuid)
                .WithMessage("InstrumentId invalid");
        });

        RuleFor(x => x.Ticker)
            .NotEmpty()
            .WithMessage("Ticker required")
            .MinimumLength(2)
            .WithMessage("Ticker invalid")
            .MaximumLength(5)
            .WithMessage("Ticker invalid");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name required")
            .MaximumLength(256)
            .WithMessage("Name invalid");

        RuleFor(x => x.Sector)
            .NotEmpty()
            .WithMessage("Sector required")
            .MaximumLength(256)
            .WithMessage("Sector invalid");
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