using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.ViewModels
{
    public class verBalanceVM
    {
        public string Pelicula { get; set; }

        [DataType(DataType.Date)]
        public DateTime Desde { get; set; }

        [DataType(DataType.Date)]
        public DateTime Hasta { get; set; }


        public double Recaudacion { get; set; }

    }
}
