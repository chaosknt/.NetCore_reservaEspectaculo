using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Data;
using ReservaEspectaculo.Models;
using ReservaEspectaculo.ViewModels;

namespace ReservaEspectaculo.Controllers
{
    public class ReservaPelicula : Controller
    {
        private readonly MiContexto _context;
        private readonly SignInManager<Usuario> _signinmanager;
        private readonly UserManager<Usuario> _userManager;

        public ReservaPelicula(MiContexto context, SignInManager<Usuario> sigin, UserManager<Usuario> usermgr)
        {
            _context = context;
            _signinmanager = sigin;
            _userManager = usermgr;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]//Recibe el id de una pelicula y devuelve una vista con la pelicula que eligio en el home + un input para elegir la fecha
        public async Task<IActionResult> ElegirFecha(int? idPelicula)
        {
            if (idPelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(idPelicula);

            if (pelicula == null)
            {
                return NotFound();
            }

            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "PeliculaId", "Titulo", pelicula.PeliculaId);

            return View();
        }

        //[HttpPost]
        public async Task<IActionResult> MostrarFunciones(FechaVM model)
        {
            //recibe una pelicula y una fecha  muestra todas las funciones disponibles y la cantidad de butacas
            //por funcion   para esa pelicula
                        
            
            var funciones = _context.Funciones.Where(f => f.Pelicula.PeliculaId == model.PeliculaId)
                                               .Where(f => f.ButacasDisponibles >= model.cantButacas)
                                               .Where(f => f.Fecha >= DateTime.Today && f.Fecha == model.Fecha)
                                               .Select(p => new MostrarFuncionesVM() { FuncionId = p.FuncionId, Fecha = p.Fecha, Hora = p.Hora, cantButacas = model.cantButacas, Titulo = p.Pelicula.Titulo, Sala = p.Sala.Numero, tipoSala = p.Sala.TipoSala.Nombre });



            if (funciones.Count() > 0)
            {
                
                 return View(funciones);
            }
            
           return RedirectToAction("sinFunciones", "Mensajes");
                                        
        }

        [HttpPost]
        public async Task<IActionResult> GuardarReserva(int cantButacas, int funcionId)
        {
            //Recibe una funcion y se la asigna a un usuario            
            
            Cliente cliente = _context.Clientes.Include(r => r.Reservas)
                                               .FirstOrDefault(c => c.NormalizedEmail == User.Identity.Name.ToUpper());
            
            var reservas = _context.Reservas.Where(r => r.ClienteId == cliente.Id).ToList();

            //var funcions = _context.Funciones.FirstOrDefault(f => f.FuncionId == reservas)
            
            if (puedeReservar(cliente.Reservas))
            {
                Funcion funcion = _context.Funciones.FirstOrDefault(f => f.FuncionId == funcionId);
                funcion.ButacasDisponibles -= cantButacas;
                _context.Funciones.Update(funcion);
                _context.SaveChanges();

                guardarReserva(cliente, cantButacas, funcionId);     
                

                return RedirectToAction("Index", "Reservas");
            }

            return RedirectToAction("reservaCancelada", "Mensajes");                                
                        
        }

        private Boolean puedeReservar(List<Reserva> reservas)
        {
            foreach (var item in reservas)
            {
                var funcion = _context.Funciones.Find(item.FuncionId);

                if(funcion.Fecha <= DateTime.Today)
                {
                    if (item.ReservaActiva)
                    {
                        item.ReservaActiva = false;
                    }                    
                }
                else
                {
                    return false;
                }                              
            }
            return true;
        }

        private void guardarReserva(Cliente cliente, int cantButacas, int funcionId)
        {
            Reserva reserva = new Reserva();
            reserva.FechaAlta = DateTime.Now;
            reserva.Cliente = cliente;
            reserva.CantidadButacas = cantButacas;
            reserva.ReservaActiva = true; // --> se podria haber setiado en el Modelo directo
            reserva.ClienteId = cliente.Id;
            reserva.FuncionId = funcionId;

            _context.Reservas.Add(reserva);
            _context.SaveChanges();

        }

    }
}
