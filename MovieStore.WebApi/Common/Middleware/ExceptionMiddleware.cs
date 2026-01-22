using System.Net;
using System.Text.Json;
using FluentValidation;

namespace MovieStore.WebApi.Common.Middleware
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

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yakalanmamış bir hata oluştu");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode status;
            object response;

            switch (exception)
            {
                case ValidationException validationException:
                    status = HttpStatusCode.BadRequest;
                    response = new
                    {
                        message = "Doğrulama hatası",
                        statusCode = (int)status,
                        errors = validationException.Errors.Select(e => e.ErrorMessage)
                    };
                    break;
                case InvalidOperationException:
                    status = HttpStatusCode.BadRequest;
                    response = new
                    {
                        message = exception.Message,
                        statusCode = (int)status
                    };
                    break;

                case KeyNotFoundException:
                    status = HttpStatusCode.NotFound;
                    response = new
                    {
                        message = exception.Message,
                        statusCode = (int)status
                    };
                    break;

                default:
                    status = HttpStatusCode.InternalServerError;
                    response = new
                    {
                        message = "Beklenmeyen bir hata oluştu",
                        statusCode = (int)status
                    };
                    break;
            }

            context.Response.StatusCode = (int)status;
            var json = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }
    }
}