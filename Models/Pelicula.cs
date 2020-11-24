using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReservaEspectaculo.Models
{
    public class Pelicula
    {
        private const String _mensajeRequerido = "El campo {0} es requerido.";

        public int PeliculaId { get; set; }
        
        [Required(ErrorMessage = _mensajeRequerido)]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de lanzamiento")]
        public DateTime FechaLanzamiento { get; set; }

        [Required(ErrorMessage = _mensajeRequerido)]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = _mensajeRequerido)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Género")]
        public int GeneroId { get; set; }

        [Display(Name = "Género")]
        public Genero Genero { get; set; }       

        public List<Funcion>  Funciones { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public string Foto { get; set; } = "default.png";

    }
}