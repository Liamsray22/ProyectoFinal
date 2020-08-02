using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Votacion
    {
        public int IdVotacion { get; set; }
        public string Cedula { get; set; }
        public int IdEleccion { get; set; }
        public int IdPartido { get; set; }

        public Ciudadanos CedulaNavigation { get; set; }
        public Elecciones IdEleccionNavigation { get; set; }
        public Partidos IdPartidoNavigation { get; set; }
    }
}
