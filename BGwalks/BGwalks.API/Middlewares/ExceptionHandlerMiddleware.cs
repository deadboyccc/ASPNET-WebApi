using System.Net;

namespace BGwalks.API.Middlewares;
class ExceptionHandlerMiddleware
{
  private readonly ILogger<ExceptionHandlerMiddleware> _logger;
  private readonly RequestDelegate _next;

  public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
  {
    _logger = logger;
    _next = next;
  }
  public async Task InvokeAsync(HttpContext httpContext)
  {
    try
    {
      // Call the next middleware or controller action.
      await _next(httpContext);
    }
    catch (Exception ex)
    {
      var errorid = Guid.NewGuid().ToString();
      // Log the exception details for debugging.
      _logger.LogError(ex, $"💣{errorid}: {ex.Message}💣"); // the logs will be lit

      // Set the HTTP response status code to 500 and return a custom error response.
      httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


      // creating the JSON object to return in the response
      var errorResponse = new
      {
        errorid,
        // FOR DEVELOPMENT PURPOSES :3
        message = ex.Message,
        stackTrace = ex.StackTrace
      };

      // Convert the error response object to JSON and write it to the response body.
      await httpContext.Response.WriteAsJsonAsync(errorResponse);

    }
  }

}