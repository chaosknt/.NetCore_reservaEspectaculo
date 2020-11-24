using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ReservaEspectaculo.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Ingrese un correo")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Ingrese su contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
