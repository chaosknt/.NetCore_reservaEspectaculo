using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservaEspectaculo.Data;
using ReservaEspectaculo.Models;
using ReservaEspectaculo.ViewModels;

namespace ReservaEspectaculo.Controllers
{
    public class CuentaClienteController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signinmanager;
        //private readonly IHostingEnvironment _hostingEnvironment;
        private readonly MiContexto _contexto;       

        public CuentaClienteController(MiContexto context, UserManager<Usuario> usrmgr, SignInManager<Usuario> signinmgr /*IHostingEnvironment hosting*/)
        {
            _contexto = context;
            _userManager = usrmgr;
            _signinmanager = signinmgr;
            //_hostingEnvironment = hosting;

        }

        public async Task<IActionResult> Index()
        {

            return RedirectToAction("Index", "Home");
        }

        public IActionResult IniciarSesion(string ReturnUrl)
        {
            TempData["returnUrl"] = ReturnUrl;
            return View();
        }

        public IActionResult AccesoDenegado()
        {
            return RedirectToAction("index", "Mensajes");
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(LoginVM model)
        {
            string returnUrl = TempData["returnUrl"] as string;

            if (ModelState.IsValid)
            {
                var resultado = await _signinmanager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (resultado.Succeeded)
                {                    
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Inicio de sesión incorrecto.");
            }
            return View(model);
        }
               
        public async Task<IActionResult> CerrarSesion()
        {
            await _signinmanager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CuentaClienteVM model)
        {
            if (ModelState.IsValid)
            {
                Cliente cliente = new Cliente();
                cliente.Email = model.Email;
                cliente.NormalizedEmail = model.Email.ToUpper();
                cliente.UserName = model.Email;
                cliente.NormalizedEmail = model.Email.ToUpper();

                var resultado = await _userManager.CreateAsync(cliente, model.Password);

                if (resultado.Succeeded)
                {
                    await _userManager.AddToRoleAsync(cliente, "Cliente");

                    await _signinmanager.SignInAsync(cliente, isPersistent: false);                   
                    return RedirectToAction("Create", "Registro", new { idUsuario = cliente.Id });
                }

                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        
        }

        public async Task<IActionResult> EmailExiste(string email)
        {
            var usuarioEncontrado = await _userManager.FindByEmailAsync(email);

            if (usuarioEncontrado != null)
            {
                
                return Json($"El correo {email} ya está en uso.");
            }
            else
            {
                
                return Json(true);
            }
        }
       

    }
}
