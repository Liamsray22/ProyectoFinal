using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Votacion
    {
        public int IdVotacion { get; set; }
        public string Cedula { get; set; }
        public int IdEleccion { get; set; }
        public int IdCandidato { get; set; }

        public Ciudadanos CedulaNavigation { get; set; }
        public Candidatos IdCandidatoNavigation { get; set; }
        public Elecciones IdEleccionNavigation { get; set; }
    }
}
