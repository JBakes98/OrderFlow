using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.S3;
using Orderflow.Options;

namespace Orderflow.Extensions;

public static class AwsExtensions
{
    public static void RegisterAwsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddTransient<IDynamoDBContext, DynamoDBContext>();

        services.AddSingleton<IAmazonS3>(config =>
        {
            var s3Config = configuration.GetSection("S3").Get<S3Options>();

            if (string.IsNullOrEmpty(s3Config?.ServiceUrl))
                return new AmazonS3Client();

            return new AmazonS3Client(new AmazonS3Config
            {
                ForcePathStyle = s3Config.ForcePathStyle,
                ServiceURL = s3Config.ServiceUrl,
                AuthenticationRegion = s3Config.AuthenticationRegion,
                UseHttp = s3Config.UseHttp
            });
        });
    }
}