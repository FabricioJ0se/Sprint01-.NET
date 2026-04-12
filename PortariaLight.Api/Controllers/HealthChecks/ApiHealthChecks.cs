using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PortariaLight.Api.HealthChecks;

/// <summary>
/// Health check customizado que verifica a saúde geral da API.
/// Pode ser expandido para verificar serviços externos (e-mail, filas, etc.).
/// </summary>
public class ApiHealthCheck : IHealthCheck
{
    private readonly ILogger<ApiHealthCheck> _logger;

    public ApiHealthCheck(ILogger<ApiHealthCheck> logger)
    {
        _logger = logger;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {

            _logger.LogInformation("Health check da API executado com sucesso.");

            var data = new Dictionary<string, object>
            {
                { "version", "2.0.0" },
                { "timestamp", DateTime.UtcNow },
                { "environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production" }
            };

            return Task.FromResult(HealthCheckResult.Healthy("API está funcionando normalmente.", data));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha no health check da API.");
            return Task.FromResult(HealthCheckResult.Unhealthy("API com problemas.", ex));
        }
    }
}