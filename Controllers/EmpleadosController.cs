using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Data;
using ReservaEspectaculo.Models;
using ReservaEspectaculo.ViewModels;

namespace ReservaEspectaculo.Controllers
{
    [Authorize(Roles = "Empleado")]
    //[Authorize]
    public class EmpleadosController : Controller
    {
        private readonly MiContexto _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signinmanager;

        public EmpleadosController(MiContexto context, UserManager<Usuario> usrmgr, SignInManager<Usuario> signinmgr)
        {
            _context = context;
            _userManager = usrmgr;
            _signinmanager = signinmgr;
        }
                
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empleados.ToListAsync());
                       
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

       // [Authorize(Roles = "Empleado")]
        public IActionResult Create()
        {
            return View();            
        }
       // [Authorize(Roles = "Empleado")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CrearEmpleadoVM model)
        {
            if (ModelState.IsValid)
            {
                Empleado empleado = new Empleado();
                Direccion direccion = new Direccion();

                crearEmpleado(model, empleado);                                

                var resultado = await _userManager.CreateAsync(empleado, model.Password);

                if (resultado.Succeeded)
                {
                    agregarDireccion(model, empleado, direccion);                    

                    await _userManager.AddToRoleAsync(empleado, "Empleado");

                    //await _signinmanager.SignInAsync(cliente, isPersistent: false);
                    return RedirectToAction("Index", "Empleados");
                }

                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);

        }

        private void crearEmpleado(CrearEmpleadoVM model, Empleado empleado)
        {
            empleado.Email = model.Email;
            empleado.NormalizedEmail = model.Email.ToUpper();
            empleado.UserName = model.Email;
            empleado.NormalizedEmail = model.Email.ToUpper();

            empleado.Nombre = model.Nombre;
            empleado.Apellido = model.Apellido;
            empleado.DNI = model.DNI;
            empleado.Telefono = model.Telefono;
            empleado.Email = model.Email;
            empleado.Password = model.Password;
            
        }

        private void agregarDireccion(CrearEmpleadoVM model, Empleado empleado, Direccion direccion)
        {
            direccion.Calle = model.Calle;
            direccion.Altura = model.Altura;
            direccion.Localidad = model.Localidad;
            direccion.Provincia = model.Provincia;
            direccion.UsuarioId = empleado.Id;

            _context.Direcciones.Add(direccion);
            _context.SaveChanges();

        }

        //[Authorize(Roles = "Empleado")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        //[Authorize(Roles = "Empleado")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string password, int telefono )
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
            {
                return NotFound();
            }

            empleado.Password = password;
            empleado.Telefono = telefono;

            _context.Empleados.Update(empleado);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

       // [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        //[Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
