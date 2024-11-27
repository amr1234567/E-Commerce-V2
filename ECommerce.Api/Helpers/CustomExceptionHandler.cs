using ECommerce.Domain.Exceptions;
using ECommerce.Services.Models.Outputs.Base;
using Microsoft.AspNetCore.Diagnostics;

namespace ECommerce.Api.Helpers
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync
            (HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionMessage = exception.Message;
            logger.LogError(
                "Error Message: {exceptionMessage}, Time of occurrence {time}",
                   exceptionMessage, DateTime.UtcNow);
            if (exception is BaseException baseException)
                await httpContext.Response.WriteAsJsonAsync(CustomResponse.Fail(baseException.FailureType, exception.Message), cancellationToken: cancellationToken);
            else
                await httpContext.Response.WriteAsJsonAsync(CustomResponse.Fail(exception.Message), cancellationToken: cancellationToken);
            return false;
        }
    }
}
