using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ReservaEspectaculo.Controllers
{
    public class MensajesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult reservaCancelada()
        {
            return View();
        }

        public IActionResult sinFunciones()
        {
            return View();
        }

        public IActionResult noCancelar()
        {
            return View();
        }
    }
}
