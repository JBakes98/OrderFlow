using System.Text;
using System.Text.Json;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Orderflow.Events.Publisher;

public class Function
{
    public void FunctionHandler(KafkaEventPayload input, ILambdaContext context)
    {
        foreach (var recordList in input.Records)
        {
            foreach (var record in recordList.Value)
            {
                // Decode the base64-encoded Key and Value
                string decodedKey = DecodeBase64(record.Key);
                string decodedValue = DecodeBase64(record.Value);

                // Deserialize the decoded JSON strings into objects
                var keySchema = JsonSerializer.Deserialize<DecodedSchema>(decodedKey);
                var valuePayload = JsonSerializer.Deserialize<DecodedPayload>(decodedValue);

                // Log or process the data
                context.Logger.LogLine($"Topic: {record.Topic}, Partition: {record.Partition}, Offset: {record.Offset}");
                context.Logger.LogLine($"Decoded Key: {JsonSerializer.Serialize(keySchema, new JsonSerializerOptions { WriteIndented = true })}");
                context.Logger.LogLine($"Decoded Value: {JsonSerializer.Serialize(valuePayload, new JsonSerializerOptions { WriteIndented = true })}");
            }
        }
    }

    private static string DecodeBase64(string base64String)
    {
        byte[] data = Convert.FromBase64String(base64String);
        return Encoding.UTF8.GetString(data);
    }
}