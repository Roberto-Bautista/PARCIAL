using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalInmobiliario.Data;
using PortalInmobiliario.Models;

namespace PortalInmobiliario.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservas/Create
        public IActionResult Create(int inmuebleId)
        {
            var reserva = new Reserva
            {
                InmuebleId = inmuebleId,
                FechaCreacion = DateTime.Now,
                FechaExpiracion = DateTime.Now.AddHours(48)
            };
            return View(reserva);
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                // 3️⃣ Validar que no haya reservas activas
                bool yaReservado = await _context.Reservas.AnyAsync(r =>
                    r.InmuebleId == reserva.InmuebleId &&
                    r.FechaExpiracion > DateTime.Now
                );

                if (yaReservado)
                {
                    ModelState.AddModelError("", "Este inmueble ya tiene una reserva activa.");
                    return View(reserva);
                }

                reserva.FechaCreacion = DateTime.Now;
                reserva.FechaExpiracion = DateTime.Now.AddHours(48);

                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Inmuebles");
            }

            return View(reserva);
        }
    }
}
