using Microsoft.EntityFrameworkCore;
using PortalInmobiliario.Models;

namespace PortalInmobiliario.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Inmueble> Inmuebles { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Visita> Visitas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Inmueble>(entity =>
            {
                entity.HasIndex(e => e.Codigo).IsUnique();
                entity.Property(e => e.Precio).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasOne(r => r.Inmueble)
                      .WithMany()
                      .HasForeignKey(r => r.InmuebleId);
            });

            modelBuilder.Entity<Visita>(entity =>
            {
                entity.HasOne(v => v.Inmueble)
                      .WithMany()
                      .HasForeignKey(v => v.InmuebleId);
            });
        }
    }
}