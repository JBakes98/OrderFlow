using OrderFlow.Contracts.Requests;
using OrderFlow.Events;
using OrderFlow.Mappers.Events;
using OrderFlow.Mappers.Request;
using OrderFlow.Mappers.Response.AlphaVantage;
using OrderFlow.Models;
using OrderFlow.Repositories;
using OrderFlow.Services;
using OrderFlow.Services.Handlers;

namespace OrderFlow.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRepository<Order>, OrderRepository>();
        services.AddScoped<IRepository<Event>, OrderEventsRepository>();
        services.AddScoped<IRepository<Instrument>, InstrumentRepository>();

        services.AddScoped<IHandler<CreateOrder, Order>, OrderCreateHandler>();
        services.AddScoped<IHandler<string, Order>, OrderGetHandler>();

        services.AddScoped<IHandler<CreateInstrument, Instrument>, InstrumentCreateHandler>();
        services.AddScoped<IHandler<string, Instrument>, InstrumentGetHandler>();

        services.AddScoped<IMapper<CreateOrder, Order>, CreateOrderToOrderMapper>();
        services.AddScoped<IMapper<Order, OrderCreatedEvent>, OrderToOrderCreatedEventMapper>();
        services.AddScoped<IMapper<CreateInstrument, Instrument>, CreateInstrumentToInstrumentMapper>();
        services.AddScoped<IMapper<BaseOrderEvent, Event>, OrderEventToEventMapper>();
        services
            .AddScoped<IMapper<OrderFlow.Contracts.Responses.AlphaVantage.GlobalQuote, GlobalQuote>,
                GlobalQuoteResponseToGlobalQuoteDomain>();

        services.AddScoped<IHandler<CreateOrder, Order>, OrderCreateHandler>();

        services.AddScoped<IEnqueueService, EnqueueService>();

        services.AddScoped<IInstrumentService, InstrumentService>();
        services.AddScoped<IOrderService, OrderService>();
    }
}