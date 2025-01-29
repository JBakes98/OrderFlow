using Orderflow.Features.Common.Models;

namespace Orderflow.Features.Orders.UploadOrderFile.Services;

public interface IUploadOrderFileService
{
    Task<Error> UploadOrderFile(IFormFile orderFile);
}