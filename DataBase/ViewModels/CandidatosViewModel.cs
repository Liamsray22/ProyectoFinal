using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.ViewModels
{
    public class CandidatosViewModel
    {
        public int IdCandidato { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdPartido { get; set; }
        public int IdPuestoElectivo { get; set; }
        public string Partido { get; set; }
        public string PuestoElectivo { get; set; }
        public string FotoPerfil { get; set; }
        public string Estado { get; set; }
        public IEnumerable<CandidatosViewModel> candidatos { get; set; }
    }
}
