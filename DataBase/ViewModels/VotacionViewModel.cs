using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.ViewModels
{
    public class VotacionViewModel
    {
        public int IdVotacion { get; set; }
        public string Cedula { get; set; }
        public int IdEleccion { get; set; }
        public int? IdCandidato { get; set; }
        public string Nombre { get; set; }
        public string PuestoElectivo { get; set; }

        public IEnumerable<CandidatosViewModel> candidatos { get; set; }

    }
}
