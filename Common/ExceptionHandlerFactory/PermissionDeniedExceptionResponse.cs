using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.ExceptionHandlerFactory
{
    internal class PermissionDeniedExceptionResponse : AbstractExceptionResponse
    {
        protected override Type FittedExceptionType => typeof(PermissionDeniedException);

        public override (string, int) GenerateResponse(Exception exception)
        {
            var httpStatusCode = StatusCodes.Status403Forbidden;
            var json = JsonConvert.SerializeObject(exception.Message);

            return (json, httpStatusCode);
        }
    }
}
