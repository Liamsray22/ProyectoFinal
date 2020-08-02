using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Ciudadanos
    {
        public Ciudadanos()
        {
            Votacion = new HashSet<Votacion>();
        }

        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }

        public ICollection<Votacion> Votacion { get; set; }
    }
}
