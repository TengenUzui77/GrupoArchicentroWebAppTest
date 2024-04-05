using GrupoArchicentroWebAppTest.Data;
using GrupoArchicentroWebAppTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace GrupoArchicentroWebAppTest.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly GrupoArchicentroWebAppTestContext _context;

        public EmpleadoController(GrupoArchicentroWebAppTestContext context)
        {
            _context = context;
        }

        static async Task<List<Empleado>> MostrarEmpleadosConRetardo(List<Empleado> empleados)
        {
            await Task.Delay(2000); // Espera de 2 segundos (simulación de operación asincrónica)

            return empleados;
        }

        // GET: Empleado
        public async Task<IActionResult> Read(string filtro = null)
        {
            try
            {
                if (_context.Empleado != null)
                {
                    //el metodo READ devuelve una lista como pide el enunciado.
                    var empleados = await _context.Empleado.ToListAsync();

                    // Verificar si se proporcionó un filtro
                    if (!string.IsNullOrEmpty(filtro))
                    {
                        // Aplicar el filtro si se proporcionó
                        //Ejercicio de codificación para filtrar una lista de objetos utilizando una expresión Linq y expresion lamda

                        empleados = empleados.Where(e => int.Parse(e.Salario)> int.Parse(filtro)).ToList();
                    }

                    return View (await MostrarEmpleadosConRetardo(empleados));
                }
                else
                {
                    throw new Exception("La entidad 'GrupoArchicentroWebAppTestContext.Empleado' es null.");
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Problem("Se produjo un error al obtener la lista de empleados. Consulte el archivo de registro para obtener más detalles.");
            }
        }


        // GET: Empleado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null || _context.Empleado == null)
                {
                    return NotFound();
                }

                var empleado = await _context.Empleado
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (empleado == null)
                {
                    return NotFound();
                }

                return View(empleado);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Problem("Se produjo un error al obtener los detalles del empleado. Consulte el archivo de registro para obtener más detalles.");
            }
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Problem("Se produjo un error al cargar la página de creación de empleado. Consulte el archivo de registro para obtener más detalles.");
            }
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Ciudad,Cargo,Salario,DNI")] Empleado empleado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(empleado);
                    await _context.SaveChangesAsync();

                    // Lanzar un nuevo hilo para notificar la creación del empleado
                    Console.WriteLine("el hilo externo es: " + Thread.CurrentThread.ManagedThreadId);
                    Task.Run(() => NotificarNuevoEmpleado(empleado));
                    Console.WriteLine("el hilo externo es: " + Thread.CurrentThread.ManagedThreadId);

                    TempData["Mensaje"] = $"El empleado {empleado.Nombre} {empleado.Apellido} ha sido creado con éxito.";

                    return RedirectToAction(nameof(Read));
                }
                return View(empleado);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Problem("Se produjo un error al crear el empleado. Consulte el archivo de registro para obtener más detalles.");
            }
        }
        private void NotificarNuevoEmpleado(Empleado empleado)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            // Lógica de notificación, en este caso, imprimir en la consola
            Console.WriteLine($"ENVIADO - Nuevo empleado creado: ID: {empleado.Id}, Nombre: {empleado.Nombre}, Apellido: {empleado.Apellido} , hilo numero: {threadId}");
        }

        // GET: Empleado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null || _context.Empleado == null)
                {
                    return NotFound();
                }

                var empleado = await _context.Empleado.FindAsync(id);
                if (empleado == null)
                {
                    return NotFound();
                }
                return View(empleado);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Problem("Se produjo un error al obtener los detalles del empleado para editar. Consulte el archivo de registro para obtener más detalles.");
            }
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Ciudad,Cargo,Salario,DNI")] Empleado empleado)
        {
            try
            {
                if (id != empleado.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(empleado);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmpleadoExists(empleado.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Read));
                }
                return View(empleado);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Problem("Se produjo un error al editar el empleado. Consulte el archivo de registro para obtener más detalles.");
            }
        }

        // GET: Empleado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null || _context.Empleado == null)
                {
                    return NotFound();
                }

                var empleado = await _context.Empleado.FirstOrDefaultAsync(m => m.Id == id);
                if (empleado == null)
                {
                    return NotFound();
                }

                return View(empleado);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Problem("Se produjo un error al obtener los detalles del empleado para eliminar. Consulte el archivo de registro para obtener más detalles.");
            }
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Empleado == null)
                {
                    return Problem("Entity set 'GrupoArchicentroWebAppTestContext.Empleado' is null.");
                }

                var empleado = await _context.Empleado.FindAsync(id);
                if (empleado != null)
                {
                    _context.Empleado.Remove(empleado);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Read));
            }
            catch (Exception ex)
            {
                LogError(ex);
                return Problem("Se produjo un error al eliminar el empleado. Consulte el archivo de registro para obtener más detalles.");
            }
        }

        private bool EmpleadoExists(int id)
        {
            try
            {
                return (_context.Empleado?.Any(e => e.Id == id)).GetValueOrDefault();
            }
            catch (Exception ex)
            {
                LogError(ex);
                return false;
            }
        }
        private void LogError(Exception ex)
        {
            string logFilePath = "error.log";
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"Error: {ex.Message}");
                writer.WriteLine($"StackTrace: {ex.StackTrace}");
                writer.WriteLine();
            }
        }
    }
}
