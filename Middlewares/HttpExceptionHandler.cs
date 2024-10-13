using Microsoft.AspNetCore.Mvc.ModelBinding;
using No_Overspend_Api.HttpExceptions;
using System.Net;

namespace No_Overspend_Api.Middlewares
{
    public class HttpExceptionHandler
    {
        private readonly RequestDelegate _next;
        public HttpExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); // Tiếp tục xử lý request
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            switch (exception)
            {
                case NotFoundException ex:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case BadRequestException ex:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            // Tùy chỉnh phản hồi lỗi
            var result = new
            {
                statusCode = context.Response.StatusCode,
                message = exception.Message,
                details = exception.Message // Bạn có thể log thêm thông tin chi tiết
            };

            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
        }
    }
}
