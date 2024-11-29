using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> logger = logger;
        private readonly IHostEnvironment env = env;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);  
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = this.env.IsDevelopment() ? new ApiExceptionModel { StatusCode = httpContext.Response.StatusCode, Message= ex.Message,Details= ex.StackTrace ?? "" } :
                    new ApiExceptionModel { StatusCode = httpContext.Response.StatusCode, Message = "Error"};

                var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, option);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
