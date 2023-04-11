using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.ExceptionHandlerFactory;

public class NullReferenceExceptionResponse : AbstractExceptionResponse
{
    protected override Type FittedExceptionType => typeof(NullReferenceException);

    public override (string, int) GenerateResponse(Exception exception)
    {
        var httpStatusCode = StatusCodes.Status404NotFound;
        var json = JsonConvert.SerializeObject(exception.Message);

        return (json, httpStatusCode);
    }
}