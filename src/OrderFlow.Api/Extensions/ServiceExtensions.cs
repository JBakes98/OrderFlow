using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using OrderFlow.Contracts.Requests;
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

        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddTransient<IDynamoDBContext, DynamoDBContext>();

        services.AddTransient<IRepository<Order>, OrderRepository>();
        services.AddTransient<IRepository<Instrument>, InstrumentRepository>();

        services.AddSingleton<IHandler<CreateOrder, Order>, OrderCreateHandler>();
        services.AddSingleton<IHandler<Guid, Order>, OrderGetHandler>();
        
        services.AddSingleton<IHandler<CreateInstrument, Instrument>, InstrumentCreateHandler>();
        services.AddSingleton<IHandler<Guid, Instrument>, InstrumentGetHandler>();
        
        services.AddSingleton<IMapper<CreateOrder, Order>, CreateOrderToOrderMapper>();
        services.AddSingleton<IMapper<CreateInstrument, Instrument>, CreateInstrumentToInstrumentMapper>();

        services.AddSingleton<IInstrumentService, InstrumentService>();
    }
}