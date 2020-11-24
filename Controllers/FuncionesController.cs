using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Data;
using ReservaEspectaculo.Models;
using ReservaEspectaculo.ViewModels;

namespace ReservaEspectaculo.Controllers
{
    //[Authorize(Roles = "Empleado, Cliente")]
    public class FuncionesController : Controller
    {
        private readonly MiContexto _context;

        public FuncionesController(MiContexto context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index(int? id)
        {
            Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Funcion, TipoSala> miContexto = default;

            if (id == null)
            {
                if (User.IsInRole("Empleado"))
                {
                    miContexto = _context.Funciones
                                          .Include(f => f.Pelicula)
                                          .Include(f => f.Sala)
                                          .Include(f => f.Pelicula.Genero)
                                          .Include(f => f.Reservas)
                                          .Include(f => f.Sala.TipoSala);
                }
                else
                {
                    miContexto = _context.Funciones.Where(f => f.Fecha >= DateTime.Today && f.Fecha <= DateTime.Today.AddDays(7))
                                                              .Include(f => f.Pelicula)
                                                              .Include(f => f.Sala)
                                                              .Include(f => f.Pelicula.Genero)
                                                              .Include(f => f.Reservas)
                                                              .Include(f => f.Sala.TipoSala);
                }
            }
            else
            {
                if (User.IsInRole("Empleado"))
                {
                    miContexto = _context.Funciones.Where(f => f.PeliculaId == id)
                                                              .Include(f => f.Pelicula)
                                                              .Include(f => f.Sala)
                                                              .Include(f => f.Pelicula.Genero)
                                                              .Include(f => f.Reservas)
                                                              .Include(f => f.Sala.TipoSala);
                }
                else
                {
                    miContexto = _context.Funciones.Where(f => f.PeliculaId == id && f.Fecha >= DateTime.Today && f.Fecha <= DateTime.Today.AddDays(7))
                                                              .Include(f => f.Pelicula)
                                                              .Include(f => f.Sala)
                                                              .Include(f => f.Pelicula.Genero)
                                                              .Include(f => f.Reservas)
                                                              .Include(f => f.Sala.TipoSala);
                }
            }

            //Intente hacer un Concat para no tener tantos if anidados pero no me salio 
            //La idea era tener la reestriccion de fecha, y si el rol es empleado 
            //concatenarle las funciones en el pasado.

            //if (User.IsInRole("Empleado"))
            //{
            //    miContexto.Concat(_context.Funciones.Where(f => f.Fecha < DateTime.Today)
            //                               .Include(f => f.Pelicula)
            //                              .Include(f => f.Sala)
            //                              .Include(f => f.Pelicula.Genero)
            //                              .Include(f => f.Reservas)
            //                              .Include(f => f.Sala.TipoSala));
            //}

            return View(await miContexto.ToListAsync());
        }               

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funciones
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .FirstOrDefaultAsync(m => m.FuncionId == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }
                
        public IActionResult Create()
        {
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "PeliculaId", "Titulo");
            ViewData["SalaId"] = new SelectList(_context.Salas, "SalaId", "Numero");
            return View();
        }                
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuncionId,Fecha,Hora,Descripcion,ButacasDisponibles,Confirmada,PeliculaId,SalaId")] Funcion funcion)
        {

            if (funcion.Fecha < DateTime.Today)
            {
               return RedirectToAction("Index", "Mensajes");
            }

            var funciones = _context.Funciones.ToList();

            if (ModelState.IsValid)
            {
                _context.Add(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "PeliculaId", "Titulo", funcion.PeliculaId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "SalaId", "Numero", funcion.SalaId);
            return View(funcion);


        }
       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funciones.FindAsync(id);
            if (funcion == null)
            {
                return NotFound();
            }
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "PeliculaId", "Titulo", funcion.PeliculaId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "SalaId", "Numero", funcion.SalaId);
            return View(funcion);
        }
               
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuncionId,Fecha,Hora,Descripcion,ButacasDisponibles,Confirmada,PeliculaId,SalaId")] Funcion funcion)
        {
            if (id != funcion.FuncionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(funcion);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
                        
            return View(funcion);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funciones
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .Include(f => f.Reservas)
                .FirstOrDefaultAsync(m => m.FuncionId == id);

            if(funcion.Reservas.Count > 0)
            {
                return RedirectToAction("noCancelar", "mensajes");
            }

            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcion = await _context.Funciones.FindAsync(id);
            _context.Funciones.Remove(funcion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionExists(int id)
        {
            return _context.Funciones.Any(e => e.FuncionId == id);
        }
       
    }
}
