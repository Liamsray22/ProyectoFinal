using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.DTO
{
    public class CandidatoDTO
    {

        public int IdCandidato { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdPartido { get; set; }
        public int IdPuestoElectivo { get; set; }
        public string FotoPerfil { get; set; }
        public string Estado { get; set; }

        public string Puesto { get; set; }

    }

}
