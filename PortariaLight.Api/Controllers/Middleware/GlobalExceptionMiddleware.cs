using System.Net;
using System.Text.Json;

namespace PortariaLight.Api.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try { await next(context); }
        catch (ArgumentException ex) { await Write(context, HttpStatusCode.BadRequest, "Argumento inválido", ex.Message); }
        catch (KeyNotFoundException ex) { await Write(context, HttpStatusCode.NotFound, "Recurso não encontrado", ex.Message); }
        catch (UnauthorizedAccessException ex) { await Write(context, HttpStatusCode.Forbidden, "Acesso negado", ex.Message); }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro não tratado");
            await Write(context, HttpStatusCode.InternalServerError, "Erro interno", "Erro inesperado. Contate o suporte.");
        }
    }

    private static async Task Write(HttpContext ctx, HttpStatusCode status, string title, string detail)
    {
        ctx.Response.ContentType = "application/problem+json";
        ctx.Response.StatusCode = (int)status;
        await ctx.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            type = $"https://httpstatuses.com/{(int)status}",
            title,
            status = (int)status,
            detail,
            instance = ctx.Request.Path.Value
        }));
    }
}