using FluentValidation;
using Orderflow.Api.Routes.Exchange.Models;
using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Api.Routes.Order.Models;
using Orderflow.Validators;

namespace Orderflow.Extensions;

public static class ValidatorExtensions
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<PostExchangeRequest>, PostExchangeRequestValidator>();
        services.AddScoped<IValidator<PostInstrumentRequest>, PostInstrumentRequestValidator>();
        services.AddScoped<IValidator<PostOrderRequest>, PostOrderRequestValidator>();
    }
}