using System.ComponentModel.DataAnnotations;

namespace PortalInmobiliario.Models
{
    public enum EstadoVisita
    {
        Solicitada,
        Confirmada,
        Cancelada
    }

    public class Visita
    {
        public int Id { get; set; }

        [Required]
        public int InmuebleId { get; set; }

        [Required]
        public string UsuarioId { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaInicio { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Compare(nameof(FechaInicio), ErrorMessage = "La fecha de fin debe ser posterior a la de inicio.")]
        public DateTime FechaFin { get; set; }

        [Required]
        public EstadoVisita Estado { get; set; }

        [StringLength(500)]
        public string? Notas { get; set; }

        // 🔗 Relaciones de navegación
        public Inmueble? Inmueble { get; set; }
    }

    
}
