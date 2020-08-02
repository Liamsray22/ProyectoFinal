using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Elecciones
    {
        public Elecciones()
        {
            Votacion = new HashSet<Votacion>();
        }

        public int IdEleccion { get; set; }
        public string Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }

        public ICollection<Votacion> Votacion { get; set; }
    }
}
