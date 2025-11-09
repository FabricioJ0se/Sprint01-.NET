using Microsoft.EntityFrameworkCore;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Repositories;
using PortariaLight.Infrastructure.Data;
using PortariaLight.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

// Database - ORACLE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IApartamentoRepository, ApartamentoRepository>();
builder.Services.AddScoped<IEncomendaRepository, EncomendaRepository>();
builder.Services.AddScoped<IMoradorRepository, MoradorRepository>();
builder.Services.AddScoped<IPortariaRepository, PortariaRepository>();
builder.Services.AddScoped<IRetiradaRepository, RetiradaRepository>();

// Services
builder.Services.AddScoped<IApartamentoService, ApartamentoService>();
builder.Services.AddScoped<IEncomendaService, EncomendaService>();
builder.Services.AddScoped<IMoradorService, MoradorService>();
builder.Services.AddScoped<IPortariaService, PortariaService>();
builder.Services.AddScoped<IRetiradaService, RetiradaService>();

// CORS - CONFIGURAÇÃO COMPLETA
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS - DEVE VIR ANTES DE AUTHORIZATION E MAPCONTROLLERS
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();