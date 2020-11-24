using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaEspectaculo.Models
{    
    public class Usuario : IdentityUser<int>
    {
            //public int UsuarioId { get; set; }

            [Required(ErrorMessage = "El campo {0} es requerido")]
            [MinLength(3, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
            [MaxLength(40, ErrorMessage = "el campo {0} debe tener como maximo {1} caracteres")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El campo {0} es requerido")]
            [MinLength(3, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
            [MaxLength(40, ErrorMessage = "el campo {0} debe tener como maximo {1} caracteres")]
            public string Apellido { get; set; }

            [Required(ErrorMessage = "El campo {0} es requerido")]
            [MinLength(6, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
            public String Password { get; set; }

            //[Required(ErrorMessage = "El campo {0} es requerido")]
            //[EmailAddress(ErrorMessage = "Error, el email debe seguir el formato nombre@dominio.algo")]
            //public string Email { get; set; }

            [Required(ErrorMessage = "El campo {0} es requerido")]
            //[RegularExpression(@"[0-9]{2}\.[0-9]{3}\.[0-9]{3}", ErrorMessage = "El campo {0} debe tener este formato NN.NNN.NNN")]
            public string DNI { get; set; }

            //[RegularExpression(@"\([0-9]{2}\)[0-9]{4}\-[0-9]{4}", ErrorMessage = "El campo {0} debe tener este formato (NN)NNNN-NNNN")]
            public int Telefono { get; set; }

            [Display(Name = "Dirección")]
            public Direccion Direccion { get; set; }
            //se comenta las expresiones regulares para no formar al usuario, validar con jquery mask number
            [DataType(DataType.Date)]
            [Display(Name = "Fecha de alta")]
            public DateTime FechaAlta { get; set; } = DateTime.Now;
    }
}

