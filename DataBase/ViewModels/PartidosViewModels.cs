using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.ViewModels
{
    public class PartidosViewModels
    {
        public int IdPartido { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Logo { get; set; }
        public string Estado { get; set; }
        public IEnumerable<PartidosViewModels> partidos { get; set; }
    }
}
