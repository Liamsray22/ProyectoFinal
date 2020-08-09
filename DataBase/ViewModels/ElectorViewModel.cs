using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.ViewModels
{
    public class ElectorViewModel
    {
        public int IdEleccion { get; set; }
        public string Nombre { get; set; }
        public IEnumerable<PuestosElectivosViewModel> puestosElectivos { get; set; }
        public List<int> votados { get; set; }

    }
}
