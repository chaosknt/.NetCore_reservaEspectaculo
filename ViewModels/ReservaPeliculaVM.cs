using ReservaEspectaculo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.ViewModels
{
    public class ReservaPeliculaVM
    {
                
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public Pelicula Pelicula { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime Fecha { get; set; }
    }
}
