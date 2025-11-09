using Microsoft.EntityFrameworkCore;
using PortariaLight.Domain.Entities;

namespace PortariaLight.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapear tabelas existentes - SEM propriedades de navegação
            modelBuilder.Entity<Apartamento>().ToTable("TPL_APARTAMENTO");
            modelBuilder.Entity<Apartamento>().HasKey(a => a.IdApartamento);
            modelBuilder.Entity<Apartamento>().Property(a => a.IdApartamento).HasColumnName("ID_APARTAMENTO");
            modelBuilder.Entity<Apartamento>().Property(a => a.Numero).HasColumnName("NUMERO");
            modelBuilder.Entity<Apartamento>().Property(a => a.Bloco).HasColumnName("BLOCO");

            modelBuilder.Entity<Morador>().ToTable("TPL_MORADOR");
            modelBuilder.Entity<Morador>().HasKey(m => m.IdMorador);
            modelBuilder.Entity<Morador>().Property(m => m.IdMorador).HasColumnName("ID_MORADOR");
            modelBuilder.Entity<Morador>().Property(m => m.Nome).HasColumnName("NOME");
            modelBuilder.Entity<Morador>().Property(m => m.Contato).HasColumnName("CONTATO");
            modelBuilder.Entity<Morador>().Property(m => m.IdApartamento).HasColumnName("ID_APARTAMENTO");

            modelBuilder.Entity<Portaria>().ToTable("TPL_PORTARIA");
            modelBuilder.Entity<Portaria>().HasKey(p => p.IdPortaria);
            modelBuilder.Entity<Portaria>().Property(p => p.IdPortaria).HasColumnName("ID_PORTARIA");
            modelBuilder.Entity<Portaria>().Property(p => p.Nome).HasColumnName("NOME_PORTEIRO");
            modelBuilder.Entity<Portaria>().Property(p => p.Turno).HasColumnName("TURNO");
            modelBuilder.Entity<Portaria>().Property(p => p.Contato).HasColumnName("CONTATO");

            modelBuilder.Entity<Retirada>().ToTable("TPL_RETIRADA");
            modelBuilder.Entity<Retirada>().HasKey(r => r.IdRetirada);
            modelBuilder.Entity<Retirada>().Property(r => r.IdRetirada).HasColumnName("ID_RETIRADA");
            modelBuilder.Entity<Retirada>().Property(r => r.DataRetirada).HasColumnName("DATA_RETIRADA");
            modelBuilder.Entity<Retirada>().Property(r => r.IdMorador).HasColumnName("ID_MORADOR");
            modelBuilder.Entity<Retirada>().Property(r => r.IdPortaria).HasColumnName("ID_PORTARIA");

            modelBuilder.Entity<Encomenda>().ToTable("TPL_ENCOMENDA");
            modelBuilder.Entity<Encomenda>().HasKey(e => e.IdEncomenda);
            modelBuilder.Entity<Encomenda>().Property(e => e.IdEncomenda).HasColumnName("ID_ENCOMENDA");
            modelBuilder.Entity<Encomenda>().Property(e => e.Descricao).HasColumnName("DESCRICAO");
            modelBuilder.Entity<Encomenda>().Property(e => e.DataRecebimento).HasColumnName("DATA_RECEBIDA");
            modelBuilder.Entity<Encomenda>().Property(e => e.IdMorador).HasColumnName("ID_MORADOR");
            modelBuilder.Entity<Encomenda>().Property(e => e.IdRetirada).HasColumnName("ID_RETIRADA");

            // Configurar relacionamentos SIMPLIFICADOS - SEM navegação
            modelBuilder.Entity<Morador>()
                .HasOne<Apartamento>()
                .WithMany()
                .HasForeignKey(m => m.IdApartamento);

            modelBuilder.Entity<Encomenda>()
                .HasOne<Morador>()
                .WithMany()
                .HasForeignKey(e => e.IdMorador);

            modelBuilder.Entity<Encomenda>()
                .HasOne<Retirada>()
                .WithMany()
                .HasForeignKey(e => e.IdRetirada);

            modelBuilder.Entity<Retirada>()
                .HasOne<Morador>()
                .WithMany()
                .HasForeignKey(r => r.IdMorador);

            modelBuilder.Entity<Retirada>()
                .HasOne<Portaria>()
                .WithMany()
                .HasForeignKey(r => r.IdPortaria);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Apartamento> Apartamentos { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<Morador> Moradores { get; set; }
        public DbSet<Portaria> Portarias { get; set; }
        public DbSet<Retirada> Retiradas { get; set; }
    }
}