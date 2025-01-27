using System.Net;

namespace Orderflow.Features.Common;

public class Error
{
    public Error(HttpStatusCode errorType, string errorCode)
        : this(errorType, new[] { errorCode })
    {
    }

    public Error(HttpStatusCode errorType, IEnumerable<string> errorCodes)
    {
        ErrorType = errorType;
        ErrorCodes = errorCodes;
    }

    public IEnumerable<string> ErrorCodes { get; }
    public HttpStatusCode ErrorType { get; }
}