using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservaEspectaculo.Models;

namespace ReservaEspectaculo.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<Rol> _rolManager;

        public RolesController(RoleManager<Rol> rolmgr)
        {
           _rolManager = rolmgr;
        }

        public async Task<IActionResult> CrearRoles()
        {
            Rol admin = new Rol() { Name = "Administrador" };
            Rol cliente = new Rol() { Name = "Cliente" };
            Rol empleado = new Rol() { Name = "Empleado" };            

            var resultado1 = await _rolManager.CreateAsync(admin);
            var resultado2 = await _rolManager.CreateAsync(cliente);
            var resultado3 = await _rolManager.CreateAsync(empleado);           

            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            ViewBag.Roles = _rolManager.Roles.ToList();
            return View();
        }
    }
}
