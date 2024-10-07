using FluentValidation;
using FluentValidation.Results;
using System.Net;
using System.Text.Json;

namespace E_Learning.ErrorHandling
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            context.Response.Clear();
            context.Response.StatusCode = SetStatusCode(ex.Errors.FirstOrDefault());
            context.Response.ContentType = "application/json";

            var errors = ex.Errors.Select(e => new
            {
                Field = e.PropertyName,
                Error = e.ErrorMessage
            });

            var result = JsonSerializer.Serialize(new { Errors = errors });
            return context.Response.WriteAsync(result);
        }

        private int SetStatusCode(ValidationFailure validation)
        {
            if (validation.ErrorCode.Contains("not found"))
                return StatusCodes.Status404NotFound;

            if (validation.ErrorCode.Contains("unauthorized"))
                return StatusCodes.Status401Unauthorized;

            return StatusCodes.Status400BadRequest;
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(new
            {
                Error = "An unexpected error occurred. Please try again later."
            });
            return context.Response.WriteAsync(result);
        }
    }
}