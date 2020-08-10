using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.DTO
{
    public class EleccionesDTO
    {

        public int IdEleccion { get; set; }
        public string Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
    }


}
