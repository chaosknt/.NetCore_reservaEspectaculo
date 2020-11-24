using System;
using System.ComponentModel.DataAnnotations;

namespace ReservaEspectaculo.Models
{
    public class Direccion
    {
        
        public int DireccionId { get; set; }

        private const String _mensajeRequerido = "El campo {0} es requerido.";

        [Required(ErrorMessage = _mensajeRequerido)]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
        [MaxLength(40, ErrorMessage = "El campo {0} debe tener como maximo {1} caracteres")]
        public string Calle { get; set; }
        
        [Required(ErrorMessage = _mensajeRequerido)]
        [Range(1,99999, ErrorMessage ="El campo {0} debe estar comprendido entre {1} y {2}")]
        public int Altura { get; set; }
        
        [Required(ErrorMessage = _mensajeRequerido)]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener como maximo {1} caracteres")]
        public string Localidad { get; set; }
        
        [Required(ErrorMessage = _mensajeRequerido)]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener como maximo {1} caracteres")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = _mensajeRequerido)]
        [Display(Name = "Titular")]
        public int UsuarioId { get; set; }

        [Display(Name = "Titular")]
        public Usuario Usuario { get; set; }

    }
}