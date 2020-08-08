using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.ViewModels
{
    public class PuestosElectivosViewModel
    {
        public int IdPuestoElectivo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public IEnumerable<PuestosElectivosViewModel> puestos { get; set; }
    }
}
