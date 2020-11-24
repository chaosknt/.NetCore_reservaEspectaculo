using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Data;
using ReservaEspectaculo.Models;


namespace ReservaEspectaculo.Controllers
{
    [Authorize(Roles = "Empleado")]
    public class GenerosController : Controller
    {
        private readonly MiContexto _context;

        public GenerosController(MiContexto context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Generos.ToList());
        }

        [ActionName("Create")]
        public IActionResult CrearGenero() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearGenero([Bind("GeneroId", "Nombre")] Genero miGenero)
        {
            if (ModelState.IsValid)
            {
                _context.Generos.Add(miGenero);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(miGenero);
        }

        [ActionName("Edit")]
        public IActionResult EditarGenero(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var genero = _context.Generos.Find(id);

            if(genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public IActionResult EditarGenero(int id, int GeneroId, string Nombre)
        {
            Genero genero = _context.Generos.Find(GeneroId);

            if(id != genero.GeneroId)
            {
                return NotFound();
            }
            
            genero.Nombre = Nombre;
            _context.Generos.Update(genero);
            _context.SaveChanges();

            return RedirectToAction("Index", "Generos");                   

        }

        [ActionName("Delete")]
        public IActionResult EliminarGenero(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = _context.Generos.FirstOrDefault(g => g.GeneroId == id);

            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult BorradoConfirmado(int id)
        {
            var genero = _context.Generos.Find(id);
            _context.Generos.Remove(genero);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool Existe(int id)
        {
            return _context.Generos.Any(e => e.GeneroId == id);
        }






    }
}
