using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.ExceptionHandlerFactory;

public class ArgumentExceptionResponse : AbstractExceptionResponse
{
    protected override Type FittedExceptionType => typeof(ArgumentException);
    public override (string, int) GenerateResponse(Exception exception)
    {
        var httpStatusCode = StatusCodes.Status400BadRequest;
        var json = JsonConvert.SerializeObject(exception.Message);

        return (json, httpStatusCode);
    }
}