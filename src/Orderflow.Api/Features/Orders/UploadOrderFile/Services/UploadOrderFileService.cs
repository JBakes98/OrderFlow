using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Ardalis.GuardClauses;
using Orderflow.Domain;
using Orderflow.Domain.Models;
using Serilog;

namespace Orderflow.Api.Routes.Order.UploadOrderFile.Services;

public class UploadOrderFileService : IUploadOrderFileService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IAmazonS3 _s3;

    public UploadOrderFileService(
        IDiagnosticContext diagnosticContext,
        IAmazonS3 s3)
    {
        _s3 = Guard.Against.Null(s3);
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
    }

    public async Task<Error> UploadOrderFile(IFormFile orderFile)
    {
        var bucketExist = await AmazonS3Util.DoesS3BucketExistV2Async(_s3, "orderflow-bulk-processing-bucket");

        if (!bucketExist)
        {
            _diagnosticContext.Set("BucketExist", false);
            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.SpecifiedBucketDoesNotExist);
        }

        _diagnosticContext.Set("BucketExist", true);

        await using var stream = orderFile.OpenReadStream();

        var request = new PutObjectRequest
        {
            BucketName = "orderflow-bulk-processing-bucket",
            Key = $"orderform-{DateTime.Now:O}",
            InputStream = stream,
            AutoCloseStream = true,
            ContentType = orderFile.ContentType
        };

        try
        {
            await _s3.PutObjectAsync(request);
        }
        catch (AmazonS3Exception e)
        {
            _diagnosticContext.Set("S3UploadError", e);
            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.OrderFileUploadFailed);
        }

        return null!;
    }
}