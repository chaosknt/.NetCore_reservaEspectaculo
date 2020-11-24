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

namespace ReservaEspectaculo.Controllers
{
    [Authorize(Roles = "Empleado")]
    public class SalasController : Controller
    {
        private readonly MiContexto _context;

        public SalasController(MiContexto context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var miContexto = _context.Salas.Include(s => s.TipoSala);
            return View(await miContexto.ToListAsync());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas
                .Include(s => s.TipoSala)
                .FirstOrDefaultAsync(m => m.SalaId == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }
                
        public IActionResult Create()
        {
            ViewData["TipoSalaId"] = new SelectList(_context.TipoSala, "TipoSalaId", "Nombre");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalaId,Numero,TipoSalaId,CapacidadButacas")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoSalaId"] = new SelectList(_context.TipoSala, "TipoSalaId", "Nombre", sala.TipoSalaId);
            return View(sala);
        }
                
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            ViewData["TipoSalaId"] = new SelectList(_context.TipoSala, "TipoSalaId", "Nombre", sala.TipoSalaId);
            return View(sala);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalaId,Numero,TipoSalaId,CapacidadButacas")] Sala sala)
        {
            if (id != sala.SalaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.SalaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoSalaId"] = new SelectList(_context.TipoSala, "TipoSalaId", "Nombre", sala.TipoSalaId);
            return View(sala);
        }

        //Segun los requerimientos nadie puede eliminar una sala. Se deja comentado por si en el futuro
        //se necesita eliminar

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sala = await _context.Salas
        //        .Include(s => s.TipoSala)
        //        .FirstOrDefaultAsync(m => m.SalaId == id);
        //    if (sala == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(sala);
        //}


        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var sala = await _context.Salas.FindAsync(id);
        //    _context.Salas.Remove(sala);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool SalaExists(int id)
        {
            return _context.Salas.Any(e => e.SalaId == id);
        }
    }
}
