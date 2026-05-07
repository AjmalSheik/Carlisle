using FluentValidation;
using System.Net;
using DLM.Application.Common.Response;

namespace DLM.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var traceId = context.TraceIdentifier;

            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation failed | TraceId: {TraceId}", traceId);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var errors = ex.Errors
                    .Select(x => $"{x.PropertyName}: {x.ErrorMessage}")
                    .ToList();

                await context.Response.WriteAsJsonAsync(
                    ApiResponse<object>.Failure(
                        "Validation failed",
                        400,
                        errors
                    )
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception | TraceId: {TraceId}", traceId);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var isDev = context.RequestServices
                    .GetRequiredService<IWebHostEnvironment>()
                    .IsDevelopment();

                var errorMessage = isDev ? ex.Message : "Internal server error";

                await context.Response.WriteAsJsonAsync(
                    ApiResponse<object>.Failure(
                        errorMessage,
                        500
                    )
                );
            }
        }
    }
}