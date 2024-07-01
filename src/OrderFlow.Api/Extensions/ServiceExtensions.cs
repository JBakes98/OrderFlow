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
        services.AddTransient<IRepository<Order>, OrderRepository>();
        services.AddTransient<IRepository<Event>, OrderEventsRepository>();
        services.AddTransient<IRepository<Instrument>, InstrumentRepository>();

        services.AddSingleton<IHandler<CreateOrder, Order>, OrderCreateHandler>();
        services.AddSingleton<IHandler<string, Order>, OrderGetHandler>();

        services.AddSingleton<IHandler<CreateInstrument, Instrument>, InstrumentCreateHandler>();
        services.AddSingleton<IHandler<string, Instrument>, InstrumentGetHandler>();

        services.AddSingleton<IMapper<CreateOrder, Order>, CreateOrderToOrderMapper>();
        services.AddSingleton<IMapper<Order, OrderCreatedEvent>, OrderToOrderCreatedEventMapper>();
        services.AddSingleton<IMapper<CreateInstrument, Instrument>, CreateInstrumentToInstrumentMapper>();
        services.AddSingleton<IMapper<BaseOrderEvent, Event>, OrderEventToEventMapper>();
        services
            .AddSingleton<IMapper<OrderFlow.Contracts.Responses.AlphaVantage.GlobalQuote, GlobalQuote>,
                GlobalQuoteResponseToGlobalQuoteDomain>();

        services.AddSingleton<IHandler<CreateOrder, Order>, OrderCreateHandler>();

        services.AddSingleton<IEnqueueService, EnqueueService>();

        services.AddSingleton<IInstrumentService, InstrumentService>();
        services.AddSingleton<IOrderService, OrderService>();
    }
}