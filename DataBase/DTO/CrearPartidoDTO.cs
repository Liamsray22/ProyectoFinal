using DataBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DataBase.DTO
{
    public class CrearPartidoDTO
    {

        public int IdPartido { get; set; }

        [Nombre(ErrorMessage = "Este Partido ya existe!")]
        [Required(ErrorMessage = "El nombre del partido debe ser llenado")]
        [StringLength(80)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del partido debe ser llenado")]
        public string Descripcion { get; set; }
        
    }

    public class NombreAttribute : ValidationAttribute
    {


        public override bool IsValid(object value)
        {

            ItlaElectorDBContext context = new ItlaElectorDBContext();

            var ListUsuario = context.Partidos.Select(x => x.Nombre).ToList();

            if (ListUsuario.Contains(value))
            {
                return false;
            }

            return true;
        }

    }



}
