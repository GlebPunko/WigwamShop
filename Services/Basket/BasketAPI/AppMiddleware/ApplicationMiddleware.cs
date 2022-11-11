using Basket.Application.CustomException;
using FluentValidation;
using System.Net;
using System.Text.Json;


namespace BasketAPI.AppMiddleware
{
    public class ApplicationMiddleware
    {
        private readonly RequestDelegate _nextRequest;
        private readonly ILogger<ApplicationMiddleware> _logger;

        public ApplicationMiddleware(RequestDelegate next, ILogger<ApplicationMiddleware> logger)
        {
            _nextRequest = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _nextRequest(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, DateTime.Now);
                await HandleExtensionAsync(context, ex);
            }
        }

        private static Task HandleExtensionAsync(HttpContext context, Exception exception)
        {
            var resultAndCode = (HttpStatusCode.InternalServerError, string.Empty);

            switch (exception)
            {
                case ValidationException validationException:
                    resultAndCode = (HttpStatusCode.UnprocessableEntity,
                        JsonSerializer.Serialize(validationException.Errors));
                    break;

                case ArgumentNullException argumentNullException:
                    resultAndCode = (HttpStatusCode.BadRequest, argumentNullException.Message);
                    break;

                case NotFoundException notFoundException:
                    resultAndCode = (HttpStatusCode.NotFound, notFoundException.Message);
                    break;

                case ArgumentException argumentException:
                    resultAndCode = (HttpStatusCode.BadRequest, argumentException.Message);
                    break;

                default:
                    resultAndCode = (HttpStatusCode.InternalServerError, exception.Message);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)resultAndCode.ToTuple().Item1;

            return context.Response.WriteAsync(resultAndCode.ToTuple().Item2);
        }
    }
}
