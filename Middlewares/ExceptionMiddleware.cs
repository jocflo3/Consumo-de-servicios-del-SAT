using System.Text.Json;
using Descargar_CFDIS.Excepciones;

namespace Descargar_CFDIS.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = ex.StatusCode;

                var response = new
                {
                    error = ex.ErrorCode,
                    message = ex.Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = 500;

                var response = new
                {
                    error = "INTERNAL_ERROR",
                    message = "Error interno: "+ex.Message 
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}