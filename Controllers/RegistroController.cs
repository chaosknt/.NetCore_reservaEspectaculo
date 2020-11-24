using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Data;
using ReservaEspectaculo.Models;
using ReservaEspectaculo.ViewModels;

namespace ReservaEspectaculo.Controllers
{
    public class RegistroController : Controller
    {
        private readonly MiContexto _miContexto;

        public RegistroController(MiContexto miContexto)
        {
            _miContexto = miContexto;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Usuarios");
        }

        public IActionResult Create(int? idUsuario)
        {
            ViewBag.Usuario = _miContexto.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            return View();
        }

        [HttpPost]
        public IActionResult Create(RegistroVM model)
        {           
            var cliente = _miContexto.Clientes.FirstOrDefault(u => u.Id == model.UsuarioId);

            crearUsuario(model, cliente);

            return RedirectToAction("Create", "Direcciones", new { idUsuario = cliente.Id}); 
           
          
        }

        private void crearUsuario(RegistroVM model, Cliente cliente)
        {
            cliente.Nombre = model.Nombre;
            cliente.Apellido = model.Apellido;
            cliente.DNI = model.DNI;
            cliente.Telefono = model.Telefono;
            
            _miContexto.Usuarios.Update(cliente);
            _miContexto.SaveChanges();
        }
    }
}
