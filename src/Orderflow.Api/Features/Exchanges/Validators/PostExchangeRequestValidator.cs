using FluentValidation;
using Orderflow.Api.Routes.Exchange.Models;
using Orderflow.Domain.Models.Enums;

namespace Orderflow.Validators;

public class PostExchangeRequestValidator : AbstractValidator<PostExchangeRequest>
{
    public PostExchangeRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name required");

        RuleFor(x => x.Abbreviation)
            .NotEmpty()
            .WithMessage("Abbreviation required")
            .MinimumLength(2)
            .WithMessage("Abbreviation invalid")
            .MaximumLength(50)
            .WithMessage("Abbreviation invalid");

        RuleFor(x => x.Mic)
            .NotEmpty()
            .WithMessage("Mic required")
            .Length(4)
            .WithMessage("Mic invalid");

        RuleFor(x => x.Region)
            .NotEmpty()
            .WithMessage("Region required");
    }
}