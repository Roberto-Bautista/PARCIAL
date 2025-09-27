using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalInmobiliario.Data;
using PortalInmobiliario.Models;

namespace PortalInmobiliario.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Catalog/Index
        public async Task<IActionResult> Index(string? ciudad, TipoInmueble? tipo, decimal? precioMin, decimal? precioMax, int? dormitorios)
        {
            try
            {
                var query = _context.Inmuebles.Where(i => i.Activo).AsQueryable();

                // Aplicar filtros solo si tienen valores
                if (!string.IsNullOrEmpty(ciudad))
                    query = query.Where(i => i.Ciudad.Contains(ciudad));

                if (tipo.HasValue)
                    query = query.Where(i => i.Tipo == tipo.Value);

                if (precioMin.HasValue && precioMin.Value > 0)
                    query = query.Where(i => i.Precio >= precioMin.Value);

                if (precioMax.HasValue && precioMax.Value > 0)
                    query = query.Where(i => i.Precio <= precioMax.Value);

                if (dormitorios.HasValue && dormitorios.Value >= 0)
                    query = query.Where(i => i.Dormitorios >= dormitorios.Value);

                var inmuebles = await query.ToListAsync();
                return View(inmuebles);
            }
            catch (Exception ex)
            {
                // Log del error para debugging
                Console.WriteLine($"Error en Index: {ex.Message}");
                ViewBag.Error = ex.Message;
                return View(new List<Inmueble>());
            }
        }

        // GET: /Catalog/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var inmueble = await _context.Inmuebles
                    .FirstOrDefaultAsync(i => i.Id == id && i.Activo);

                if (inmueble == null)
                {
                    return NotFound();
                }

                return View(inmueble);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.ToString();
                return View("Error");
            }
        }
    }
}