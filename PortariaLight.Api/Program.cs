using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Repositories;
using PortariaLight.Infrastructure.Data;
using PortariaLight.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configura CORS (libera tudo para testes locais)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// 🔹 Configura o DbContext para o Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Injeção de dependências (Repositories + Services)
builder.Services.AddScoped<IEncomendaRepository, EncomendaRepository>();
builder.Services.AddScoped<IEncomendaService, EncomendaService>();

builder.Services.AddScoped<IMoradorRepository, MoradorRepository>();
builder.Services.AddScoped<IMoradorService, MoradorService>();

builder.Services.AddScoped<IPortariaRepository, PortariaRepository>();
builder.Services.AddScoped<IPortariaService, PortariaService>();

builder.Services.AddScoped<IRetiradaRepository, RetiradaRepository>();
builder.Services.AddScoped<IRetiradaService, RetiradaService>();

// 🔹 Adiciona controllers e documentação do Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PortariaLight API",
        Version = "v1",
        Description = "API para gerenciamento de portaria, moradores, encomendas e retiradas."
    });

    // 🔹 (Opcional, mas profissional) — inclui os comentários XML no Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// 🔹 Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PortariaLight API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
