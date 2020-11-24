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
    [Authorize(Roles = "Cliente, Empleado")]
    public class ClientesController : Controller
    {
        private readonly MiContexto _context;

        public ClientesController(MiContexto context)
        {
            _context = context;
        }
                
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Usuarios");
        }

        public IActionResult MostrarDatos()
        {
            if (User.IsInRole("Cliente"))
            {
                var cliente = _context.Clientes.Include(c => c.Direccion).FirstOrDefault(c => c.NormalizedEmail == User.Identity.Name.ToUpper());

                if (cliente == null)
                {
                    return NotFound();
                }
                return View(cliente);                
            }
            
            return View("Index","Mensajes");
        }

        public IActionResult ActualizarTelefono(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var cliente = _context.Clientes.Find(id);

            if(cliente == null)
            {
                return NotFound();
            }
            return View(new ActualizarTelefonoVM { Telefono = cliente.Telefono });
        }

        [HttpPost]
        public IActionResult ActualizarTelefono(ActualizarTelefonoVM model)
        {
            var cliente = _context.Clientes.Include(c => c.Direccion).FirstOrDefault(c => c.NormalizedEmail == User.Identity.Name.ToUpper());
            cliente.Telefono = model.Telefono;
            _context.Update(cliente);
            _context.SaveChanges();

            return RedirectToAction("MostrarDatos", "Clientes");

        }
                
    }
}
