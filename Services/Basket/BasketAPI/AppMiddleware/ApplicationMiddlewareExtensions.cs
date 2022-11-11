namespace BasketAPI.AppMiddleware
{
    public static class ApplicationMiddlewareExtensions
    {
        public static IApplicationBuilder UseApplicationExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApplicationMiddleware>();
        }
    }
}
