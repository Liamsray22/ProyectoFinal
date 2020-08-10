using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.DTO
{
    public class VotacionDTO
    {        
        public string Nombre { get; set; }
        public string PuestoElectivo { get; set; }
        public string Partido { get; set; }
    }
}
