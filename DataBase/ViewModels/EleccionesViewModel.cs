using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.ViewModels
{
    public class EleccionesViewModel
    {
        public int IdEleccion { get; set; }
        public string Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
        public IEnumerable<EleccionesViewModel> elecciones { get; set; }
    }
}
