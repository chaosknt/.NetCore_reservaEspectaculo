using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReservaEspectaculo.Models
{
    public class Cliente : Usuario
     
    {
        //public int ClienteId { get; set; }
        public List<Reserva> Reservas { get; set; }


    }
}