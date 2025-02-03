using FluentValidation;
using Orderflow.Features.Exchanges.CreateExchange.Contracts;
using Orderflow.Features.Exchanges.CreateExchange.Validators;
using Orderflow.Features.Instruments.CreateInstrument.Contracts;
using Orderflow.Features.Instruments.CreateInstrument.Validators;
using Orderflow.Features.Orders.CreateOrder.Contracts;
using Orderflow.Features.Orders.CreateOrder.Validators;

namespace Orderflow.Common.Extensions;

public static class ValidatorExtensions
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<PostExchangeRequest>, PostExchangeRequestValidator>();
        services.AddScoped<IValidator<PostInstrumentRequest>, PostInstrumentRequestValidator>();
        services.AddScoped<IValidator<PostOrderRequest>, PostOrderRequestValidator>();
    }
}