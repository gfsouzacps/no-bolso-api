using MediatR;
using NoBolso.Domain.Enums;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace NoBolso.API.Middlewares;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
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
        context.Response.ContentType = "application/json";
        var responseStatusCode = HttpStatusCode.InternalServerError;
        object response;

        switch (exception)
        {
            case ValidationException validationException:
                responseStatusCode = HttpStatusCode.BadRequest;
                response = new
                {
                    title = "Erro de Validação",
                    status = (int)responseStatusCode,
                    errors = validationException.Errors.GroupBy(e => e.PropertyName)
                                                       .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage))
                };
                break;
            default:
                response = new { error = "Ocorreu um erro inesperado.", details = exception.Message };
                break;
        }

        context.Response.StatusCode = (int)responseStatusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}