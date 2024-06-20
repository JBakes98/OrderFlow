using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace OrderFlow.Extensions;

public static class AwsExtensions
{
    public static void RegisterAwsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddTransient<IDynamoDBContext, DynamoDBContext>(); 
    }
}