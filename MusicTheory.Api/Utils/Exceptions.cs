using Microsoft.AspNetCore.Http;
using System;

namespace MusicTheory.Api.Utils;

/// <summary>
/// Custom exception class for handling application-specific errors
/// </summary>
public class CustomException : Exception
{
    /*
     // Example use in a controller method:
       if (someErrorCondition)
       {
           throw new CustomException("A custom error occurred.", StatusCodes.Status400BadRequest);
       }
     */
    /// <summary>
    /// Gets the HTTP status code associated with the exception
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomException"/> class with a specified error message and status code
    /// </summary>
    /// <param name="message">The error message that describes the error</param>
    /// <param name="statusCode">The HTTP status code associated with the error</param>
    public CustomException(string message, int statusCode = StatusCodes.Status500InternalServerError)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
