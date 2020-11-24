using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservaEspectaculo.Models
{
    public class Funcion
    {

        public int FuncionId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Hora de la función")]
        public DateTime Hora { get; set; }

        [MinLength(5, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
        [MaxLength(500, ErrorMessage = "El campo {0} debe tener como maximo {1} caracteres")]
        [Display(Name = "Descripción")]
        public String Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Butacas disponibles")]
        public int ButacasDisponibles { get; set; }

        //[NotMapped]
        //public int cantButacas { get; set; }

        public Boolean Confirmada { get; set; }        
        
        [Display(Name ="Película")]
        public int PeliculaId { get; set; }

        public Pelicula Pelicula { get; set; }

        [Display(Name = "Sala")]
        public int SalaId { get; set; }

        public Sala Sala { get; set; }

        public List<Reserva> Reservas { get; set; }

        public double Recaudacion()
        {
            int contador = 0;

            foreach (var item in Reservas)
            {
                contador += item.CantidadButacas;
            }
            return contador * Sala.TipoSala.Precio;
        }
	
        
        

    }
}