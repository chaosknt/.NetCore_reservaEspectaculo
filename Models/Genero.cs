using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Models
{
    public class Genero
    {
        
        public int GeneroId { get; set; }

        [MinLength(3,ErrorMessage ="El campo {0} debe tener al menos {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int PeliculaId { get; set; }
        public List<Pelicula> Peliculas { get; set; }
              

    }
}
