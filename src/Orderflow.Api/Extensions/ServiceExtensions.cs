using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Api.Routes.Order.Models;
using Orderflow.Data.Entities;
using Orderflow.Data.Repositories;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Orderflow.Events;
using Orderflow.Mappers.Data;
using Orderflow.Mappers.Domain;
using Orderflow.Mappers.Events;
using Orderflow.Mappers.Request;
using Orderflow.Mappers.Response;
using Orderflow.Mappers.Response.AlphaVantage;
using Orderflow.Services;
using Orderflow.Services.AlphaVantage.Api.Responses;

namespace Orderflow.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEventMapperFactory, EventMapperFactory>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IInstrumentRepository, InstrumentRepository>();

        services.AddScoped<IInstrumentService, InstrumentService>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<IMapper<PostOrderRequest, Order>, PostOrderRequestToOrderMapper>();
        services.AddScoped<IMapper<Order, GetOrderResponse>, OrderToGetOrderResponseMapper>();
        services.AddScoped<IMapper<Order, OrderEntity>, OrderDomainToOrderDataMapper>();
        services.AddScoped<IMapper<OrderEntity, Order>, OrderDataToOrderDomainMapper>();
        services.AddScoped<IMapper<Order, OrderRaisedEvent>, OrderToOrderRaisedEventMapper>();

        services.AddScoped<IMapper<PostInstrumentRequest, Instrument>, PostInstrumentRequestToInstrumentMapper>();
        services.AddScoped<IMapper<Instrument, GetInstrumentResponse>, InstrumentToGetInstrumentResponseMapper>();
        services.AddScoped<IMapper<Instrument, InstrumentEntity>, InstrumentDomainToInstrumentDataMapper>();
        services.AddScoped<IMapper<InstrumentEntity, Instrument>, InstrumentDataToInstrumentDomainMapper>();
        services.AddScoped<IMapper<Instrument, InstrumentCreatedEvent>, InstrumentToInstrumentCreatedEventMapper>();

        services
            .AddScoped<IMapper<GetGlobalQuoteResponse, GlobalQuote>,
                GlobalQuoteResponseToGlobalQuoteDomainMapper>();
    }
}