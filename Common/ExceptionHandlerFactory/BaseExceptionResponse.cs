using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.ExceptionHandlerFactory;

public class BaseExceptionResponse : AbstractExceptionResponse
{
    protected override Type FittedExceptionType => typeof(ArgumentException);
    protected override bool IsHandlerFor(Type exceptionType)
    {
        return true;
    }

    public override (string, int) GenerateResponse(Exception exception)
    {
        var httpStatusCode = StatusCodes.Status500InternalServerError;

        var json = JsonConvert.SerializeObject(exception);

        return (json, httpStatusCode);
    }
}