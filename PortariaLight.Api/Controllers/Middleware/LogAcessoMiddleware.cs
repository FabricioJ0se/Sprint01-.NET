using PortariaLight.Domain.Entities;
using PortariaLight.Domain.Repositories;
using System.Diagnostics;

namespace PortariaLight.Api.Middleware;

public class LogAcessoMiddleware(RequestDelegate next, ILogger<LogAcessoMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context, ILogAcessoRepository logRepo)
    {
        var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault()
                         ?? Guid.NewGuid().ToString();
        context.Response.Headers["X-Correlation-ID"] = correlationId;

        var sw = Stopwatch.StartNew();
        await next(context);
        sw.Stop();

        try
        {
            await logRepo.InserirAsync(new LogAcesso
            {
                Endpoint = context.Request.Path,
                Metodo = context.Request.Method,
                StatusCode = context.Response.StatusCode,
                UsuarioNome = context.User?.Identity?.Name ?? "anonimo",
                CorrelationId = correlationId,
                DuracaoMs = sw.ElapsedMilliseconds,
                IpOrigem = context.Connection.RemoteIpAddress?.ToString()
            });
        }
        catch (Exception ex) { logger.LogWarning(ex, "Falha ao gravar log no MongoDB"); }
    }
}
