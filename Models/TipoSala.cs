using System;
using System.ComponentModel.DataAnnotations;

namespace ReservaEspectaculo.Models
{
    public class TipoSala
    {
        public int TipoSalaId { get; set; }

        [MinLength(3, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
        [Display(Name = "Tipo de sala")]
        public string Nombre { get; set; }
                
        [DataType(DataType.Currency)]
        [Range(0.0, Double.MaxValue, ErrorMessage = "El campo {0} Debe ser mayor que {1}.")]
        public double Precio { get; set; }
    }
}