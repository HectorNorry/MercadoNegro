using MercadoNegro.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace MercadoNegro.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor para inyección de dependencias
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets para tus entidades
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación Usuario-Movimiento (Remitente)
            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Remitente)
                .WithMany(u => u.MovimientosEnviados)
                .HasForeignKey(m => m.RemitenteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación Usuario-Movimiento (Destinatario)
            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Destinatario)
                .WithMany(u => u.MovimientosRecibidos)
                .HasForeignKey(m => m.DestinatarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de campos únicos
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Cvu)
                .IsUnique();

            // Configuración de tipos decimales
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Saldo)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Movimiento>()
                .Property(m => m.Monto)
                .HasColumnType("decimal(18,2)");
        }
    }

    // Clase factory para las migraciones (solo necesaria para diseño)
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=MercadoNegroDB;Trusted_Connection=True;TrustServerCertificate=True;"); // <-- ¡CAMBIO AQUI!

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}