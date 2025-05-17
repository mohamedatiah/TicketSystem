using System.Net;
using System.Text.Json;
using TransVault.Common;
using TransVault.Domain.Common;

namespace TransVault.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BaseTransVaultException ex)
            {
                await HandleTransVaultExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new TransVaultResponse<string>()
            {
                HttpStatusCode = HttpStatusCode.InternalServerError,
                Message = exception.Message,
                InternalError = exception?.InnerException?.ToString()
            };

            // Set the response
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.HttpStatusCode;

            // Serialize the response
            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
        private async Task HandleTransVaultExceptionAsync(HttpContext context, BaseTransVaultException exception)
        {
            var response = new TransVaultResponse<string>()
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Message = exception.Message,
                InternalError = exception?.InnerException?.ToString() //for dev only
            };

            // Set the response
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.HttpStatusCode;

            // Serialize the response
            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}