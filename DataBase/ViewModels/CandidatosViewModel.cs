using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
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

        public string PuestoElectivo { get; set; }
        public string FotoPerfil { get; set; }

        [Required(ErrorMessage = "El candidato debe identificarse con una foto")]
        public IFormFile Photo { get; set; }
        public string Estado { get; set; }
        public IEnumerable<CandidatosViewModel> candidatos { get; set; }
    }
}
