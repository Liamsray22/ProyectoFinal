using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.ViewModels
{
    public class PuestosElectivosViewModel
    {
        public int IdPuestoElectivo { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [Remote(action: "VerifyPuesto", controller: "PuestosElectivos")]

        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripcion del Puesto es requerida")]

        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public IEnumerable<PuestosElectivosViewModel> puestos { get; set; }
    }
}
