using Orderflow.Domain.Models;

namespace Orderflow.Api.Routes.Order.UploadOrderFile.Services;

public interface IUploadOrderFileService
{
    Task<Error> UploadOrderFile(IFormFile orderFile);
}