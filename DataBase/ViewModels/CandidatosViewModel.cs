using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DataBase.ViewModels
{
    public class CandidatosViewModel
    {
        public int IdCandidato { get; set; }
        [Required(ErrorMessage = "Debe ingresar nombre")]

        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe ingresar apellido")]

        public string Apellido { get; set; }
        [Required(ErrorMessage = "El candidato debe tener un partido")]

        public int IdPartido { get; set; }
        [Required(ErrorMessage = "Debe seleccionar el puesto electivo")]

        public int IdPuestoElectivo { get; set; }
        public List<PartidosViewModels> ListPartido { get; set; }
        public List<PuestosElectivosViewModel> ListPuestoElectivo { get; set; }
        public string Partido { get; set; }
        [Required(ErrorMessage = "Debe ingresar nombre de candidato")]
        public string Nombreedit { get; set; }
        public string PuestoElectivo { get; set; }
        public string FotoPerfil { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una foto")]
        public string newfoto { get; set; }

        //[Remote(action: "Verifyphoto", controller: "Candidatos")]
        public IFormFile Photo { get; set; }
        public string Estado { get; set; }
        public IEnumerable<CandidatosViewModel> candidatos { get; set; }
    }
}
