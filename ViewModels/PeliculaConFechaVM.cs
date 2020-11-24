using ReservaEspectaculo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.ViewModels
{
    public class PeliculaConFechaVM
    {
        [Display(Name = "Pelicula")]
        public int PeliculaId { get; set; }

        public Pelicula Pelicula { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Display(Name = "Cantidad de butacas")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1,250)]
        public int CantButacas { get; set; } = 1;
    }
}

