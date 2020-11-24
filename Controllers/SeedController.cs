using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReservaEspectaculo.Data;
using ReservaEspectaculo.Models;
using ReservaEspectaculo.ViewModels;

namespace ReservaEspectaculo.Controllers
{
    public class SeedController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signinmanager;        
        private readonly MiContexto _contexto;

        public SeedController(MiContexto context, UserManager<Usuario> usrmgr, SignInManager<Usuario> signinmgr)
        {
            _contexto = context;
            _userManager = usrmgr;
            _signinmanager = signinmgr;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public  async Task<IActionResult> Cargar()
        {
            PeliculaYFuncion();
            Empleado empleado = new Empleado();

            empleado.Email = "angel@ort.com";
            empleado.NormalizedEmail = empleado.Email.ToUpper();
            empleado.UserName =empleado.Email;
            empleado.NormalizedEmail = empleado.Email.ToUpper();

            empleado.Nombre = "Angel";
            empleado.Apellido = "Diaz";
            empleado.DNI = "3121";
            empleado.Telefono = 13441212;            
            empleado.Password = "Mariano123_";

            var resultado =  await _userManager.CreateAsync(empleado, empleado.Password);
            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(empleado, "Empleado");
                return RedirectToAction("Index", "Home");
            }

            return NotFound();

        }                      

        private void PeliculaYFuncion()
        {
            TipoSala normal = new TipoSala()
            {
                Nombre = "Normal",
                Precio = 900
            };

            TipoSala premium = new TipoSala()
            {
                Nombre = "Premium",
                Precio = 3900
            };

            _contexto.TipoSala.Add(normal);
            _contexto.TipoSala.Add(premium);

            Sala sala1 = new Sala()
            {
                Numero = 918,
                TipoSala = normal,
                CapacidadButacas = 250

            };

            Sala sala2 = new Sala()
            {
                Numero = 918,
                TipoSala = premium,
                CapacidadButacas = 250

            };

            _contexto.Salas.Add(sala1);
            _contexto.Salas.Add(sala2);


            Genero genero1 = new Genero { Nombre = "Comedia" };
            Genero genero2 = new Genero { Nombre = "Accion" };

            _contexto.Generos.Add(genero1);
            _contexto.Generos.Add(genero2);
            
            Pelicula pelicula1 = new Pelicula()
            {
                Titulo = "Mi pobre angelito",
                Descripcion = "Un niño se queda solo en navidad",
                Genero = genero1,
                Foto = "SinFoto",
                FechaLanzamiento = new DateTime(1991, 1, 10)
            };

            
            Pelicula pelicula2 = new Pelicula()
            {
                Titulo = "Depredador",
                Descripcion = "Un furtivo monstruo alienígena ataca a varios comandos durante una misión en las selvas de América Central.",
                Foto = "SinFoto",
                Genero = genero2,
                FechaLanzamiento = new DateTime(1987, 8, 18)
            };

            _contexto.Peliculas.Add(pelicula1);
            _contexto.Peliculas.Add(pelicula2);                      

            Funcion funcion1 = new Funcion()
            {
                Fecha = new DateTime(2020, 12, 18),
                Hora = DateTime.Now,
                Descripcion = "Una pelicula para toda la familia",
                ButacasDisponibles = 250,
                Confirmada = true,
                Pelicula = pelicula1,
                Sala = sala1
            };

            Funcion funcion2 = new Funcion()
            {
                Fecha = new DateTime(2020, 12, 18),
                Hora = DateTime.Now,
                Descripcion = "Una pelicula para pocos...",
                ButacasDisponibles = 250,
                Confirmada = true,
                Pelicula = pelicula2,
                Sala = sala2
            };

            _contexto.Funciones.Add(funcion1);
            _contexto.Funciones.Add(funcion2);

            _contexto.SaveChanges();
        }

       
    }
}
