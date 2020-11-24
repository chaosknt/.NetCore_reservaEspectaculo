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
    
    public class DireccionesController : Controller
    {
        private readonly MiContexto _context;

        public DireccionesController(MiContexto context)
        {
            _context = context;
        }
                
        public async Task<IActionResult> Index()
        {
            var miContexto = _context.Direcciones.Include(d => d.Usuario);
            return View(await miContexto.ToListAsync());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.DireccionId == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }
                
        public IActionResult Create(int? idUsuario)
        {
            
            if (idUsuario == null)
            {
                ViewBag.UsuarioId = new SelectList(_context.Usuarios, "Id", "Apellido");
            }
            else
            {
                ViewBag.Usuario = _context.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DireccionId,Calle,Altura,Localidad,Provincia,UsuarioId")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(direccion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Apellido", direccion.UsuarioId);
            return View(direccion);
          
        }
                
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }
            //ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Apellido", direccion.UsuarioId);
            ViewBag.Usuario = _context.Usuarios.FirstOrDefault(u => u.Id == direccion.UsuarioId);

            return View(direccion);
        }
               
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DireccionId,Calle,Altura,Localidad,Provincia,UsuarioId")] Direccion direccion)
        {
            if (id != direccion.DireccionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccionExists(direccion.DireccionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (User.IsInRole("Cliente"))
                {
                    return RedirectToAction("MostrarDatos", "Clientes");
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Apellido", direccion.UsuarioId);
            return View(direccion);
        }
                
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.DireccionId == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }
               
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var direccion = await _context.Direcciones.FindAsync(id);
            _context.Direcciones.Remove(direccion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DireccionExists(int id)
        {
            return _context.Direcciones.Any(e => e.DireccionId == id);
        }
    }
}
