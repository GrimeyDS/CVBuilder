using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Authentication;

namespace CVBuilder.Middleware;

public class ExceptionHandlingMiddleware(IProblemDetailsService problemDetailsService, IWebHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = (exception is ValidationException validationException)
            ? CreateValidationProblemDetails(validationException)
            : CreateProblemDetails(exception, environment);

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
            Exception = exception
        });

    }

    private static ProblemDetails CreateProblemDetails(Exception exception, IWebHostEnvironment environment)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Exception error",
            Detail = exception.Message,
            Type = GetExceptionStatusCode(exception)
        };

        if (environment.IsDevelopment())
            problemDetails.Extensions.Add("StackTrace", exception.StackTrace);

        return problemDetails;
    }

    private static ValidationProblemDetails CreateValidationProblemDetails(ValidationException validationException)
    {
        var problemDetails = new ValidationProblemDetails()
        {
            Title = "Validation error",
            Detail = "One ore more validation error(s) have occurred.",
            Errors = validationException.Errors
        };

        return problemDetails;
    }

    private static string GetExceptionStatusCode(Exception exception)
    {
        HttpStatusCode statusCode = exception switch
        {
            NotImplementedException => HttpStatusCode.NotImplemented,
            ValidationException => HttpStatusCode.BadRequest,
            ArgumentException => HttpStatusCode.BadRequest,
            UnauthorizedAccessException => HttpStatusCode.Forbidden,
            KeyNotFoundException => HttpStatusCode.NotFound,
            AuthenticationException => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };

        return statusCode.ToString();
    }
}
