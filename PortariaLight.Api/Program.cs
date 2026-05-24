using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using PortariaLight.Api.HealthChecks;
using PortariaLight.Api.Middleware;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Repositories;
using PortariaLight.Infrastructure.Data;
using PortariaLight.Infrastructure.Repositories;
using Serilog;
using Serilog.Events;
using System.Text;

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
    Log.Information("Passo 1 - Iniciando builder");
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    Log.Information("Passo 2 - Controllers");
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddHttpContextAccessor();

    Log.Information("Passo 3 - Swagger");
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "PortariaLight API",
            Version = "v1",
            Description = "API para gerenciamento de portaria, moradores, encomendas e retiradas."
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Informe: Bearer {token}"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                Array.Empty<string>()
            }
        });
    });

    Log.Information("Passo 4 - Oracle");
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

    Log.Information("Passo 5 - MongoDB");
    builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection("MongoDbSettings"));
    builder.Services.AddScoped<ILogAcessoRepository, LogAcessoRepository>();

    Log.Information("Passo 6 - Repositories e Services");
    builder.Services.AddScoped<IApartamentoRepository, ApartamentoRepository>();
    builder.Services.AddScoped<IEncomendaRepository, EncomendaRepository>();
    builder.Services.AddScoped<IMoradorRepository, MoradorRepository>();
    builder.Services.AddScoped<IPortariaRepository, PortariaRepository>();
    builder.Services.AddScoped<IRetiradaRepository, RetiradaRepository>();
    builder.Services.AddScoped<IApartamentoService, ApartamentoService>();
    builder.Services.AddScoped<IEncomendaService, EncomendaService>();
    builder.Services.AddScoped<IMoradorService, MoradorService>();
    builder.Services.AddScoped<IPortariaService, PortariaService>();
    builder.Services.AddScoped<IRetiradaService, RetiradaService>();

    Log.Information("Passo 7 - JWT");
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);
    builder.Services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ClockSkew = TimeSpan.Zero
            };
        });
    builder.Services.AddAuthorization();

    Log.Information("Passo 8 - CORS");
    builder.Services.AddCors(options =>
        options.AddPolicy("AllowAll", policy =>
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

    Log.Information("Passo 9 - Health Checks");
    builder.Services.AddHealthChecks()
        .AddDbContextCheck<AppDbContext>(
            name: "oracle-db",
            failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
            tags: new[] { "database", "oracle" })
        .AddCheck<ApiHealthCheck>(
            name: "api-self",
            tags: new[] { "api" });

    Log.Information("Passo 10 - OpenTelemetry");
    builder.Services.AddOpenTelemetry()
        .ConfigureResource(resource => resource
            .AddService(serviceName: "PortariaLight.Api", serviceVersion: "2.0.0"))
        .WithTracing(tracing => tracing
            .AddAspNetCoreInstrumentation(opts => { opts.RecordException = true; })
            .AddEntityFrameworkCoreInstrumentation()
            .AddConsoleExporter())
        .WithMetrics(metrics => metrics
            .AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation()
            .AddProcessInstrumentation()
            .AddPrometheusExporter());

    Log.Information("Passo 11 - Build");
    var app = builder.Build();

    Log.Information("Passo 12 - Pipeline");
    app.UseMiddleware<GlobalExceptionMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "PortariaLight API v1");
            c.RoutePrefix = string.Empty;
        });
    }

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

    app.MapPrometheusScrapingEndpoint();
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
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<LogAcessoMiddleware>();
    app.MapControllers();

    Log.Information("Passo 13 - Run");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro: {Message}", ex.Message);
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }