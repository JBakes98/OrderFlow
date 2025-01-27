using Orderflow.Features.Common;

namespace Orderflow.Features.Orders.UploadOrderFile.Services;

public interface IUploadOrderFileService
{
    Task<Error> UploadOrderFile(IFormFile orderFile);
}