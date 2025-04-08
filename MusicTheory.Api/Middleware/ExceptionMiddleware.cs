using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MusicTheory.Api.Utils;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusicTheory.Api.Middleware;

/// <summary>
/// Middleware to handle exceptions globally
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline</param>
    /// <param name="logger">Logger instance for logging exceptions</param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware to handle exceptions
    /// </summary>
    /// <param name="context">HTTP context</param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CustomException ex)
        {
            _logger.LogWarning(ex, "A custom exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        var result = JsonSerializer.Serialize(new { error = message });
        return context.Response.WriteAsync(result);
    }
}
