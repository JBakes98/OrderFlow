using OrderFlow.Contracts.Requests;
using OrderFlow.Data.Entities;
using OrderFlow.Data.Repositories;
using OrderFlow.Data.Repositories.Interfaces;
using OrderFlow.Domain.Models;
using OrderFlow.Events;
using OrderFlow.Mappers.Data;
using OrderFlow.Mappers.Domain;
using OrderFlow.Mappers.Events;
using OrderFlow.Mappers.Request;
using OrderFlow.Mappers.Response.AlphaVantage;
using OrderFlow.Services;
using OrderFlow.Services.Handlers;

namespace OrderFlow.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEventMapperFactory, EventMapperFactory>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IInstrumentRepository, InstrumentRepository>();

        services.AddScoped<IInstrumentService, InstrumentService>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<IHandler<CreateOrder, Order>, OrderCreateHandler>();

        services.AddScoped<IHandler<CreateInstrument, Instrument>, InstrumentCreateHandler>();

        services.AddScoped<IMapper<CreateOrder, Order>, CreateOrderToOrderMapper>();
        services.AddScoped<IMapper<Order, OrderEntity>, OrderDomainToOrderDataMapper>();
        services.AddScoped<IMapper<OrderEntity, Order>, OrderDataToOrderDomainMapper>();
        services.AddScoped<IMapper<Order, OrderRaisedEvent>, OrderToOrderRaisedEventMapper>();

        services.AddScoped<IMapper<CreateInstrument, Instrument>, CreateInstrumentToInstrumentMapper>();
        services.AddScoped<IMapper<Instrument, InstrumentEntity>, InstrumentDomainToInstrumentDataMapper>();
        services.AddScoped<IMapper<InstrumentEntity, Instrument>, InstrumentDataToInstrumentDomainMapper>();
        services.AddScoped<IMapper<Instrument, InstrumentCreatedEvent>, InstrumentToInstrumentCreatedEventMapper>();

        services
            .AddScoped<IMapper<OrderFlow.Contracts.Responses.AlphaVantage.GlobalQuote, GlobalQuote>,
                GlobalQuoteResponseToGlobalQuoteDomainMapper>();
    }
}