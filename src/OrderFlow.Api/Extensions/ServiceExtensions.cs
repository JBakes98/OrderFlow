using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using OrderFlow.Contracts.Requests;
using OrderFlow.Mappers.Request;
using OrderFlow.Models;
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

        services.AddSingleton<IOrderHandler<Order>, CreateHandler>();
        services.AddSingleton<IOrderHandler<Guid>, GetHandler>();
        services.AddSingleton<IMapper<CreateOrder, Order>, CreateOrderToOrderMapper>();
    }
}