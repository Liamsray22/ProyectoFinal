using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class PuestoElectivo
    {
        public PuestoElectivo()
        {
            Candidatos = new HashSet<Candidatos>();
        }

        public int IdPuestoElectivo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        public ICollection<Candidatos> Candidatos { get; set; }
    }
}
