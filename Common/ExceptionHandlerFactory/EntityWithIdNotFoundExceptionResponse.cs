using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.ExceptionHandlerFactory;

public class EntityWithIdNotFoundExceptionResponse : AbstractExceptionResponse
{
    protected override Type FittedExceptionType => typeof(EntityNotFoundException);

    public override (string, int) GenerateResponse(Exception exception)
    {
        var httpStatusCode = StatusCodes.Status404NotFound;
        var json = JsonConvert.SerializeObject(exception.Message);

        return (json, httpStatusCode);
    }
}