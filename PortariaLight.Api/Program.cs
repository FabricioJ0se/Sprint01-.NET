using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Repositories;
using PortariaLight.Infrastructure.Data;
using PortariaLight.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy
            .WithOrigins("http://localhost:5204", "https://localhost:7169")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEncomendaRepository, EncomendaRepository>();
builder.Services.AddScoped<IEncomendaService, EncomendaService>();

builder.Services.AddScoped<IMoradorRepository, MoradorRepository>();

builder.Services.AddScoped<IPortariaRepository, PortariaRepository>();

builder.Services.AddScoped<IRetiradaRepository, RetiradaRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PortariaLight API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PortariaLight API v1"));
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();
