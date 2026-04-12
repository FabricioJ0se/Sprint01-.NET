using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PortariaLight.Domain.Entities;
using PortariaLight.Infrastructure.Data;

namespace PortariaLight.Tests.Integration.Fixtures;

public class PortariaLightWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = "PortariaLightTestDb";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
        
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(_dbName));

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
            SeedTestData(db);
        });

        builder.UseEnvironment("Development");
    }

    private static void SeedTestData(AppDbContext db)
    {
        if (db.Apartamentos.Any()) return;

        db.Apartamentos.AddRange(
            new Apartamento { IdApartamento = 1, Numero = "101", Bloco = "A" },
            new Apartamento { IdApartamento = 2, Numero = "202", Bloco = "B" }
        );
        db.SaveChanges();

        db.Moradores.AddRange(
            new Morador { IdMorador = 1, Nome = "João Teste", Contato = "11999990001", IdApartamento = 1 },
            new Morador { IdMorador = 2, Nome = "Maria Teste", Contato = "11999990002", IdApartamento = 2 }
        );
        db.SaveChanges();

        db.Retiradas.Add(
            new Retirada { IdRetirada = 1, DataRetirada = DateTime.Today, IdMorador = 1, IdPortaria = 0 }
        );
        db.SaveChanges();

        db.Encomendas.Add(
            new Encomenda { IdEncomenda = 1, Descricao = "Caixa de Teste", IdMorador = 1, IdRetirada = 1, DataRecebimento = DateTime.Today }
        );
        db.SaveChanges();
    }
}