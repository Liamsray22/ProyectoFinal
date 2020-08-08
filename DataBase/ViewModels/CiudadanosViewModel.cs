using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.ViewModels
{
    public class CiudadanosViewModel
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
        public IEnumerable<CiudadanosViewModel> ciudadanos { get; set; }
    }
}
