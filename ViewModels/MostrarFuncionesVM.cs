using ReservaEspectaculo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.ViewModels
{
    public class MostrarFuncionesVM
    {
        public int FuncionId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Hora de la función")]
        public DateTime Hora { get; set; }
                      

        public int cantButacas { get; set; }

        public string Titulo { get; set; }

        [Display(Name = "Sala")]
        public int Sala { get; set; }

        public string tipoSala  { get; set; }
        
    }
}
