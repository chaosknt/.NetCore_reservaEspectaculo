using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.ViewModels
{
    public class CuentaClienteVM
    {
        private const String _mensajeRequerido = "El campo {0} es requerido.";

        [Required(ErrorMessage = _mensajeRequerido)]
        [EmailAddress]  
        [Remote(action: "EmailExiste", controller: "CuentaCliente")]
        public string Email { get; set; }

        [Required(ErrorMessage = _mensajeRequerido)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = _mensajeRequerido)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmación de Password")]
        [Compare("Password", ErrorMessage = "La password de confirmación no es igual. Por favor, verifiquela.")]
        public string ConfirmacionPassword { get; set; }
                
    }
}
