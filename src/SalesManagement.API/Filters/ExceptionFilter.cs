using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "Erro interno do servidor.";

        if (context.Exception is ArgumentException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = context.Exception.Message;
        }
        else if (context.Exception is KeyNotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            message = context.Exception.Message;
        }
        else if (context.Exception is InvalidOperationException)
        {
            statusCode = HttpStatusCode.Conflict;
            message = context.Exception.Message;
        }

        context.Result = new ObjectResult(new { error = message })
        {
            StatusCode = (int)statusCode
        };

        context.ExceptionHandled = true;
    }
}
