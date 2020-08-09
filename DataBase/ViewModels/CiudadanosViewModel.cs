using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.ViewModels
{
    public class CiudadanosViewModel
    {
        [StringLength(11, ErrorMessage = "Solo 11 digitos de la Cedula")]
        [Required(ErrorMessage = "La Cedula es requerida")]

        public string Cedula { get; set; }
        [Required(ErrorMessage = "El nombre es requeridao")]

        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Apellido es requerido")]

        public string Apellido { get; set; }
        [Required(ErrorMessage = "El Email es requerido")]
        [EmailAddress(ErrorMessage = "Email invalido")]

        public string Email { get; set; }
        public string Estado { get; set; }
        public IEnumerable<CiudadanosViewModel> ciudadanos { get; set; }
    }
}
