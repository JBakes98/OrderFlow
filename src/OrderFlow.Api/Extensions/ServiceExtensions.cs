using OrderFlow.Contracts.Requests;
using OrderFlow.Events;
using OrderFlow.Mappers.Events;
using OrderFlow.Mappers.Request;
using OrderFlow.Models;
using OrderFlow.Repositories;
using OrderFlow.Services;
using OrderFlow.Services.Handlers;

namespace OrderFlow.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();

        services.AddTransient<IRepository<Order>, OrderRepository>();
        services.AddTransient<IRepository<Event>, OrderEventsRepository>();
        services.AddTransient<IRepository<Instrument>, InstrumentRepository>();

        services.AddScoped<IHandler<CreateOrder, Order>, OrderCreateHandler>();
        services.AddScoped<IHandler<Guid, Order>, OrderGetHandler>();

        services.AddScoped<IHandler<CreateInstrument, Instrument>, InstrumentCreateHandler>();
        services.AddScoped<IHandler<Guid, Instrument>, InstrumentGetHandler>();

        services.AddSingleton<IMapper<CreateOrder, Order>, CreateOrderToOrderMapper>();
        services.AddSingleton<IMapper<Order, OrderCreatedEvent>, OrderToOrderCreatedEventMapper>();
        services.AddSingleton<IMapper<CreateInstrument, Instrument>, CreateInstrumentToInstrumentMapper>();
        services.AddSingleton<IMapper<BaseOrderEvent, Event>, OrderEventToEventMapper>();

        services.AddScoped<IHandler<CreateOrder, Order>, OrderCreateHandler>();

        services.AddScoped<IInstrumentService, InstrumentService>();
        services.AddScoped<IOrderService, OrderService>();
    }
}