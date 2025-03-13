using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace SalesManagement.API.Helpers
{
    public static class ExceptionHandler
    {
        public static IActionResult HandleException(Exception ex, string defaultErrorMessage = "Erro interno ao processar a requisição.")
        {
            return ex switch
            {
                KeyNotFoundException => new NotFoundObjectResult(new { error = ex.Message }),
                ArgumentException => new BadRequestObjectResult(new { error = ex.Message }),
                InvalidOperationException => new ConflictObjectResult(new { error = ex.Message }),
                _ => new ObjectResult(new { error = defaultErrorMessage, details = ex.Message }) { StatusCode = 500 }
            };
        }
    }
}
