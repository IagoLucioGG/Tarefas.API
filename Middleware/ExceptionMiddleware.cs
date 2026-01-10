using System.Net;
using Tarefas.API.Domain;
using Tarefas.API.Exceptions;


namespace Tarefas.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
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
            catch (NotFoundException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.NotFound,
                    ex.Message);
            }
            catch (ConflictException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.Conflict,
                    ex.Message);
            }
            catch (UnauthorizathionException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.Unauthorized,
                    ex.Message);
            }
            catch (ArgumentException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado");

                await HandleException(
                    context,
                    HttpStatusCode.InternalServerError,
                    "Erro interno no servidor");
            }
        }

        private static async Task HandleException(
            HttpContext context,
            HttpStatusCode statusCode,
            string mensagem)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = ResponseModel<object>.Erro(mensagem);

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
