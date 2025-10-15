using Microsoft.EntityFrameworkCore;
using PortariaLight.Domain.Entities;

namespace PortariaLight.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets para cada tabela
        public DbSet<Morador> Moradores { get; set; }
        public DbSet<Portaria> Portarias { get; set; }
        public DbSet<Retirada> Retiradas { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeamento de Morador
            modelBuilder.Entity<Morador>(entity =>
            {
                entity.ToTable("TLP_MORADOR");
                entity.HasKey(e => e.IdMorador).HasName("TLP_MORADOR_PK");

                entity.Property(e => e.IdMorador).HasColumnName("ID_MORADOR");
                entity.Property(e => e.Nome).HasColumnName("NOMR").HasMaxLength(100);
                entity.Property(e => e.Apartamento).HasColumnName("APARTAMENTO");
                entity.Property(e => e.Bloco).HasColumnName("BLOCO");
                entity.Property(e => e.Contato).HasColumnName("CONTATO").HasMaxLength(100);
            });

            // Mapeamento de Portaria
            modelBuilder.Entity<Portaria>(entity =>
            {
                entity.ToTable("TLP_PORTARIA");
                entity.HasKey(e => e.IdPortaria).HasName("TLP_PORTARIA_PK");

                entity.Property(e => e.IdPortaria).HasColumnName("ID_PORTARIA");
                entity.Property(e => e.NomePorteiro).HasColumnName("NOME_PORTEIRO").HasMaxLength(100);
                entity.Property(e => e.Turno).HasColumnName("TURNO").HasMaxLength(50);
                entity.Property(e => e.Contato).HasColumnName("CONTATO").HasMaxLength(100);
                entity.Property(e => e.DataRegistro).HasColumnName("DATA_REGISTRO");
            });

            // Mapeamento de Retirada
            modelBuilder.Entity<Retirada>(entity =>
            {
                entity.ToTable("TLP_RETIRADA");
                entity.HasKey(e => e.IdRetirada).HasName("TLP_RETIRADA_PK");

                entity.Property(e => e.IdRetirada).HasColumnName("ID_RETIRADA");
                entity.Property(e => e.DataRetirada).HasColumnName("DATA_RETIRADA");
                entity.Property(e => e.TokenRetirada).HasColumnName("TOKEN_RETIRADA").HasMaxLength(50);
                entity.Property(e => e.MoradorId).HasColumnName("TLP_MORADOR_ID_MORADOR");
                entity.Property(e => e.PortariaId).HasColumnName("TLP_PORTARIA_ID_PORTARIA");

                entity.HasOne(e => e.Morador)
                      .WithMany(m => m.Retiradas)
                      .HasForeignKey(e => e.MoradorId)
                      .HasConstraintName("TLP_RETIRADA_TLP_MORADOR_FK");

                entity.HasOne(e => e.Portaria)
                      .WithMany(p => p.Retiradas)
                      .HasForeignKey(e => e.PortariaId)
                      .HasConstraintName("TLP_RETIRADA_TLP_PORTARIA_FK");
            });

            // Mapeamento de Encomenda
            modelBuilder.Entity<Encomenda>(entity =>
            {
                entity.ToTable("TLP_ENCOMENDA");
                entity.HasKey(e => e.IdEncomenda).HasName("TLP_ENCOMENDA_PK");

                entity.Property(e => e.IdEncomenda).HasColumnName("ID_ENCOMENDA");
                entity.Property(e => e.Descricao).HasColumnName("DESCRICAO").HasMaxLength(200);
                entity.Property(e => e.DataRecebida).HasColumnName("DATA_RECEBIDA");
                entity.Property(e => e.Status).HasColumnName("STATUS").HasMaxLength(50);
                entity.Property(e => e.MoradorId).HasColumnName("TLP_MORADOR_ID_MORADOR");
                entity.Property(e => e.RetiradaId).HasColumnName("TLP_RETIRADA_ID_RETIRADA");

                entity.HasOne(e => e.Morador)
                      .WithMany(m => m.Encomendas)
                      .HasForeignKey(e => e.MoradorId)
                      .HasConstraintName("TLP_ENCOMENDA_TLP_MORADOR_FK");

                entity.HasOne(e => e.Retirada)
                      .WithMany(r => r.Encomendas)
                      .HasForeignKey(e => e.RetiradaId)
                      .HasConstraintName("TLP_ENCOMENDA_TLP_RETIRADA_FK");
            });
        }
    }
}
