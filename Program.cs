using Microsoft.EntityFrameworkCore;
using PortalInmobiliario.Data;
using PortalInmobiliario.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Agregar datos de prueba de manera segura
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    try
    {
        // Verificar si hay inmuebles
        var count = context.Inmuebles.Count();
        Console.WriteLine($"Inmuebles existentes: {count}");
        
        // Solo agregar datos si no hay ninguno
        if (count == 0)
        {
            var inmuebles = new List<Inmueble>
            {
                new Inmueble
                {
                    Codigo = "INM001",
                    Titulo = "Departamento Centro Lima",
                    Tipo = TipoInmueble.Departamento,
                    Ciudad = "Lima",
                    Direccion = "Av. Arequipa 123",
                    Dormitorios = 2,
                    Banos = 1,
                    MetrosCuadrados = 80,
                    Precio = 250000m,
                    Activo = true
                },
                new Inmueble
                {
                    Codigo = "INM002",
                    Titulo = "Casa Familiar Arequipa",
                    Tipo = TipoInmueble.Casa,
                    Ciudad = "Arequipa",
                    Direccion = "Calle Las Flores 456",
                    Dormitorios = 3,
                    Banos = 2,
                    MetrosCuadrados = 120,
                    Precio = 350000m,
                    Activo = true
                },
                new Inmueble
                {
                    Codigo = "INM003",
                    Titulo = "Oficina Comercial",
                    Tipo = TipoInmueble.Oficina,
                    Ciudad = "Lima",
                    Direccion = "Jr. Lampa 789",
                    Dormitorios = 0,
                    Banos = 1,
                    MetrosCuadrados = 50,
                    Precio = 180000m,
                    Activo = true
                }
            };

            context.Inmuebles.AddRange(inmuebles);
            await context.SaveChangesAsync();
            
            Console.WriteLine($"Se agregaron {inmuebles.Count} inmuebles de prueba.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al manejar datos: {ex.Message}");
        Console.WriteLine($"Detalle: {ex.InnerException?.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}");

app.Run();