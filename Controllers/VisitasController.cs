using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalInmobiliario.Data;
using PortalInmobiliario.Models;

namespace PortalInmobiliario.Controllers
{
    public class VisitasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Visitas/Create
        public IActionResult Create(int inmuebleId)
        {
            var visita = new Visita { InmuebleId = inmuebleId };
            return View(visita);
        }

        // POST: Visitas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Visita visita)
        {
            if (ModelState.IsValid)
            {
                // 1️⃣ Validar que FechaInicio < FechaFin
                if (visita.FechaInicio >= visita.FechaFin)
                {
                    ModelState.AddModelError("", "La fecha de inicio debe ser anterior a la fecha de fin.");
                    return View(visita);
                }

                // 2️⃣ Validar que no haya visitas solapadas
                bool solapada = await _context.Visitas.AnyAsync(v =>
                    v.InmuebleId == visita.InmuebleId &&
                    v.Estado != EstadoVisita.Cancelada &&
                    ((visita.FechaInicio >= v.FechaInicio && visita.FechaInicio < v.FechaFin) ||
                     (visita.FechaFin > v.FechaInicio && visita.FechaFin <= v.FechaFin) ||
                     (visita.FechaInicio <= v.FechaInicio && visita.FechaFin >= v.FechaFin))
                );

                if (solapada)
                {
                    ModelState.AddModelError("", "Ya existe una visita en ese rango de fechas para este inmueble.");
                    return View(visita);
                }

                _context.Add(visita);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Inmuebles"); // redirige al catálogo
            }

            return View(visita);
        }
    }
}
