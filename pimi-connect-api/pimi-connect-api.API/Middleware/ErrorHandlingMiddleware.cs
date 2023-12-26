using pimi_connect_api.Exceptions;
using pimi_connect_api.Exceptions.Base;

namespace pimi_connect_api.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (BadRequest400Exception e)
        {
            MakeExceptionResponse(context, e);
        }
        catch (NotFound404Exception e)
        {
            MakeExceptionResponse(context, e);
        }
        catch (Unauthorized401Exception e)
        {
            MakeExceptionResponse(context, e);            
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync($"Something went wrong :(");
        }
    }

    private static async void MakeExceptionResponse(HttpContext context, CustomExceptionBase exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        await context.Response.WriteAsync(exception.UserMessage);
    }
}