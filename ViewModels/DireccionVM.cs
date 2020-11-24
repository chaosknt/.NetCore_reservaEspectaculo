using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.ViewModels
{
    public class DireccionVM
    {
        private const String _mensajeRequerido = "El campo {0} es requerido.";

        [Required(ErrorMessage = _mensajeRequerido)]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
        [MaxLength(40, ErrorMessage = "el campo {0} debe tener como maximo {1} caracteres")]
        public string Calle { get; set; }

        [Required(ErrorMessage = _mensajeRequerido)]
        [Range(1, 99999, ErrorMessage = "El campo {0} debe estar comprendido entre {1} y {2}")]
        public int Altura { get; set; }

        [Required(ErrorMessage = _mensajeRequerido)]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
        [MaxLength(30, ErrorMessage = "el campo {0} debe tener como maximo {1} caracteres")]
        public string Localidad { get; set; }

        [Required(ErrorMessage = _mensajeRequerido)]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
        [MaxLength(30, ErrorMessage = "el campo {0} debe tener como maximo {1} caracteres")]
        public string Provincia { get; set; }

    }
}
