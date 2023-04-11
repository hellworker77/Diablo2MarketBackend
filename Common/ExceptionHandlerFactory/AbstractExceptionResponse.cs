using Microsoft.Extensions.DependencyInjection;

namespace Common.ExceptionHandlerFactory;

public abstract class AbstractExceptionResponse
{
    protected abstract Type FittedExceptionType { get; }

    protected virtual bool IsHandlerFor(Type exceptionType)
    {
        return FittedExceptionType == exceptionType;
    }

    public abstract (string json, int httpStatusCode) GenerateResponse(Exception exception);

    public static Func<IServiceProvider, Func<Type, AbstractExceptionResponse>> GetResponseHandler =>
        provider => type =>
        {
            var responseHandler = provider.GetServices<AbstractExceptionResponse>().First(x => x.IsHandlerFor(type));

            return responseHandler;
        };
}