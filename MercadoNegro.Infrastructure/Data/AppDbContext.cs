using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using MercadoNegro.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MercadoNegro.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Remitente -> Movimiento
            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Remitente)
                .WithMany(u => u.MovimientosEnviados)
                .HasForeignKey(m => m.RemitenteId)
                .OnDelete(DeleteBehavior.Restrict); // No eliminar remitente si se elimina movimiento

            modelBuilder.Entity<Movimiento>()
            .Property(m => m.Monto)
            .HasColumnType("decimal(18,2)");


            // Relación Destinatario -> Movimiento
            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Destinatario)
                .WithMany(u => u.MovimientosRecibidos)
                .HasForeignKey(m => m.DestinatarioId)
                .OnDelete(DeleteBehavior.Restrict); // Igual

            // CVU único
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Cvu)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
            .Property(u => u.Saldo)
            .HasColumnType("decimal(18,2)");
        }
    }
}
