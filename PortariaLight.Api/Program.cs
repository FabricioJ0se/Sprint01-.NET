using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Repositories;
using PortariaLight.Infrastructure.Data;
using PortariaLight.Infrastructure.Repositories;
using PortariaLight.Api.HealthChecks;
using Serilog;
using Serilog.Events;

// --- Serilog: logging estruturado --------------------------------------------
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "PortariaLight.Api")
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {CorrelationId} {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/portarialight-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {CorrelationId} {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Iniciando PortariaLight.Api");

    var builder = WebApplication.CreateBuilder(args);

    // --- Serilog como provider de log ----------------------------------------
    builder.Host.UseSerilog();

    // --- Controllers + JSON --------------------------------------------------
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "PortariaLight API",
            Version = "v1",
            Description = "API para gerenciamento de portaria, moradores, encomendas e retiradas."
        });
    });
    builder.Services.AddHttpContextAccessor();

    // --- Oracle Database ------------------------------------------------------
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

    // --- Repositories --------------------------------------------------------
    builder.Services.AddScoped<IApartamentoRepository, ApartamentoRepository>();
    builder.Services.AddScoped<IEncomendaRepository, EncomendaRepository>();
    builder.Services.AddScoped<IMoradorRepository, MoradorRepository>();
    builder.Services.AddScoped<IPortariaRepository, PortariaRepository>();
    builder.Services.AddScoped<IRetiradaRepository, RetiradaRepository>();

    // --- Services ------------------------------------------------------------
    builder.Services.AddScoped<IApartamentoService, ApartamentoService>();
    builder.Services.AddScoped<IEncomendaService, EncomendaService>();
    builder.Services.AddScoped<IMoradorService, MoradorService>();
    builder.Services.AddScoped<IPortariaService, PortariaService>();
    builder.Services.AddScoped<IRetiradaService, RetiradaService>();

    // --- CORS ----------------------------------------------------------------
    builder.Services.AddCors(options =>
        options.AddPolicy("AllowAll", policy =>
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

    // --- Health Checks -------------------------------------------------------
    builder.Services.AddHealthChecks()
        .AddDbContextCheck<AppDbContext>(
            name: "oracle-db",
            failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
            tags: new[] { "database", "oracle" })
        .AddCheck<ApiHealthCheck>(
            name: "api-self",
            tags: new[] { "api" });

    // --- OpenTelemetry: tracing + métricas ----------------------------------
    builder.Services.AddOpenTelemetry()
        .ConfigureResource(resource => resource
            .AddService(
                serviceName: "PortariaLight.Api",
                serviceVersion: "2.0.0"))
        .WithTracing(tracing => tracing
            .AddAspNetCoreInstrumentation(opts =>
            {
                opts.RecordException = true;
            })
            .AddEntityFrameworkCoreInstrumentation()
            .AddConsoleExporter())
        .WithMetrics(metrics => metrics
            .AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation()
            .AddProcessInstrumentation()
            .AddPrometheusExporter());

    // --- Build ---------------------------------------------------------------
    var app = builder.Build();

    // --- Pipeline HTTP -------------------------------------------------------
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "PortariaLight API v1");
            c.RoutePrefix = string.Empty;
        });
    }

    // Middleware de correlação de requisições (adiciona CorrelationId nos logs)
    app.Use(async (context, next) =>
    {
        var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault()
                            ?? Guid.NewGuid().ToString();
        context.Items["CorrelationId"] = correlationId;
        context.Response.Headers["X-Correlation-ID"] = correlationId;

        using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
        {
            await next();
        }
    });

    app.UseSerilogRequestLogging(opts =>
    {
        opts.MessageTemplate =
            "HTTP {RequestMethod} {RequestPath} respondeu {StatusCode} em {Elapsed:0.0000} ms";
    });

    // Expõe métricas Prometheus em /metrics
    app.MapPrometheusScrapingEndpoint();

    // Health check endpoints
    app.MapHealthChecks("/health");
    app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("database")
    });
    app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("api")
    });

    app.UseCors("AllowAll");
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Aplicação encerrada inesperadamente");
}
finally
{
    Log.CloseAndFlush();
}

// Necessário para WebApplicationFactory nos testes de integração
public partial class Program { }