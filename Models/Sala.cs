using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Models
{
    public class Sala
    {        
        public int SalaId { get; set; }

        [Range(0.0, Double.MaxValue, ErrorMessage = "El campo {0} Debe ser mayor que {1}.")]
        //[Display(Name = "Número")]
        [Display(Name ="Número de sala")]
        public int Numero { get; set; }
                
        [Display(Name = "Tipo de sala")]
        public TipoSala TipoSala { get; set; }

        [Display(Name = "Tipo de sala")]
        public int TipoSalaId { get; set; }

        [Range(0.0, Double.MaxValue, ErrorMessage = "El campo {0} Debe ser mayor que {1}.")]
        [Display(Name = "Cantidad de Butacas")]
        public int CapacidadButacas { get; set; }

        public List<Funcion> Funciones { get; set; }
    }
}
