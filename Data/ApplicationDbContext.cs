using Microsoft.EntityFrameworkCore;
using PortalInmobiliario.Models;

namespace PortalInmobiliario.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 🔹 DbSets
        public DbSet<Inmueble> Inmuebles { get; set; }
        public DbSet<Visita> Visitas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Restricciones en base de datos
            builder.Entity<Inmueble>()
                .HasCheckConstraint("CK_Inmueble_Precio", "Precio > 0")
                .HasCheckConstraint("CK_Inmueble_MetrosCuadrados", "MetrosCuadrados > 0");

            builder.Entity<Visita>()
                .HasCheckConstraint("CK_Visita_Fechas", "FechaInicio < FechaFin");

            // Semilla mínima de datos
            builder.Entity<Inmueble>().HasData(
                new Inmueble { Id = 1, Codigo = "DEP001", Titulo = "Depa Miraflores", Tipo = TipoInmueble.Departamento, Ciudad = "Lima", Direccion = "Av. Larco 123", Dormitorios = 2, Banos = 2, MetrosCuadrados = 80, Precio = 150000, Activo = true },
                new Inmueble { Id = 2, Codigo = "CAS001", Titulo = "Casa Arequipa", Tipo = TipoInmueble.Casa, Ciudad = "Arequipa", Direccion = "Calle Real 456", Dormitorios = 3, Banos = 2, MetrosCuadrados = 120, Precio = 250000, Activo = true },
                new Inmueble { Id = 3, Codigo = "OFC001", Titulo = "Oficina San Isidro", Tipo = TipoInmueble.Oficina, Ciudad = "Lima", Direccion = "Av. Javier Prado 789", Dormitorios = 0, Banos = 1, MetrosCuadrados = 60, Precio = 180000, Activo = true }
            );
        }
    }
}
