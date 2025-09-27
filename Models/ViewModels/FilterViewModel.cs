using System.ComponentModel.DataAnnotations;

namespace PortalInmobiliario.Models.ViewModels
{
    public class FilterViewModel
    {
        public string? Ciudad { get; set; }
        
        public TipoInmueble? Tipo { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "El precio mínimo debe ser mayor o igual a 0")]
        public decimal? PrecioMin { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "El precio máximo debe ser mayor o igual a 0")]
        public decimal? PrecioMax { get; set; }
        
        [Range(0, 20, ErrorMessage = "El número de dormitorios debe estar entre 0 y 20")]
        public int? Dormitorios { get; set; }
        
        public int Pagina { get; set; } = 1;
    }
}