using DataBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DataBase.ViewModels
{
    public class CiudadanosViewModel
    {

      // [Cedula(ErrorMessage = "Esta cédula ya a sido registrada")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Debes insertar exactamente 11 digitos de la Cedula")]
        [Required(ErrorMessage = "La Cedula es requerida")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]

        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Apellido es requerido")]

        public string Apellido { get; set; }
        [Required(ErrorMessage = "El Email es requerido")]
        [EmailAddress(ErrorMessage = "Email invalido")]

        public string Email { get; set; }
        public string Estado { get; set; }
        public IEnumerable<CiudadanosViewModel> ciudadanos { get; set; }
    }

 /*   public class CedulaAttribute : ValidationAttribute
    {


        public override bool IsValid(object value)
        {

            ItlaElectorDBContext context = new ItlaElectorDBContext();

            var ListUsuario = context.Ciudadanos.Select(x => x.Cedula).ToList();

            if (ListUsuario.Contains(value))
            {
                return false;
            }

            return true;
        }

    }*/



}
