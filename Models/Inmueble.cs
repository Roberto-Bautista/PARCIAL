using System.ComponentModel.DataAnnotations;

namespace PortalInmobiliario.Models
{
    public enum TipoInmueble { Departamento, Casa, Oficina, Local }

    public class Inmueble
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        public string Titulo { get; set; } = string.Empty;

        public string? Imagen { get; set; }

        [Required]
        public TipoInmueble Tipo { get; set; }

        [Required]
        public string Ciudad { get; set; } = string.Empty;

        [Required]
        public string Direccion { get; set; } = string.Empty;

        [Range(0, 20)]
        public int Dormitorios { get; set; }

        [Range(0, 20)]
        public int Banos { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Los metros cuadrados deben ser > 0")]
        public int MetrosCuadrados { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser > 0")]
        public decimal Precio { get; set; }

        public bool Activo { get; set; } = true;

    }
}
