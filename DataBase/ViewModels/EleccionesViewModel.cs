using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.ViewModels
{
    public class EleccionesViewModel
    {
        public int IdEleccion { get; set; }
        [Required(ErrorMessage = "El nombre de la eleccion es requerido")]

        public string Nombre { get; set; }


        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy, hh.mm tt}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Debe elegir la fecha de la Elecciones")]
        [DataType(DataType.Date)]
        //[Remote(action: "Verifydate", controller: "Elecciones")]
        public DateTime Fecha { get; set; }

        public string Estado { get; set; }
        public IEnumerable<EleccionesViewModel> elecciones { get; set; }
    }
}
