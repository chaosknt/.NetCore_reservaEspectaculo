using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaEspectaculo.Models
{
    public class Empleado : Usuario
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        

        //public String Legajo { get; set; }
        public Guid Legajo { get; set; } = Guid.NewGuid();

       

    }
}

   
