using Common.ExceptionHandlerFactory;

namespace Trading.Web.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Func<Type, AbstractExceptionResponse> _responseHandlerFactory;

    public ExceptionHandlerMiddleware(RequestDelegate next,
        Func<Type, AbstractExceptionResponse> responseHandlerFactory)
    {
        _next = next;
        _responseHandlerFactory = responseHandlerFactory;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (Exception exception)
        {
            var responseHandler = _responseHandlerFactory(exception.GetType());

            var result = responseHandler.GenerateResponse(exception);

            httpContext.Response.StatusCode = result.httpStatusCode;

            await httpContext.Response.WriteAsJsonAsync(result.json);
        }
    }
}