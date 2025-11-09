using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortariaLight.Web;
using PortariaLight.Web.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5204/api/")
    });

// Registrar os modelos
builder.Services.AddScoped<Morador>();
builder.Services.AddScoped<Encomenda>();
builder.Services.AddScoped<Apartamento>();
builder.Services.AddScoped<Portaria>();
builder.Services.AddScoped<Retirada>();

await builder.Build().RunAsync();