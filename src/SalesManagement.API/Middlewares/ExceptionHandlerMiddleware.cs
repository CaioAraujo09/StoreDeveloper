using System.Net;
using System.Text.Json;

namespace SalesManagement.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new
            {
                type = "InternalServerError",
                error = "Ocorreu um erro ao processar a solicitação.",
                detail = exception.Message
            };

            switch (exception)
            {
                case KeyNotFoundException _:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse = new { type = "ResourceNotFound", error = "Recurso não encontrado", detail = exception.Message };
                    break;

                case ArgumentException _:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse = new { type = "ValidationError", error = "Erro de validação", detail = exception.Message };
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var json = JsonSerializer.Serialize(errorResponse);
            return response.WriteAsync(json);
        }
    }
}
