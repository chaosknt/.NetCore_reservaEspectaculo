using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.ViewModels
{
    public class RegistroVM
    {        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
        [MaxLength(40, ErrorMessage = "el campo {0} debe tener como maximo {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinLength(3, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
        [MaxLength(40, ErrorMessage = "el campo {0} debe tener como maximo {1} caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]       
        public string DNI { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Telefono { get; set; }

        public int UsuarioId { get; set; }

    }
}
