using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Candidatos
    {
        public Candidatos()
        {
            Votacion = new HashSet<Votacion>();
        }

        public int IdCandidato { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdPartido { get; set; }
        public int IdPuestoElectivo { get; set; }
        public string FotoPerfil { get; set; }
        public string Estado { get; set; }

        public Partidos IdPartidoNavigation { get; set; }
        public PuestoElectivo IdPuestoElectivoNavigation { get; set; }
        public ICollection<Votacion> Votacion { get; set; }
    }
}
