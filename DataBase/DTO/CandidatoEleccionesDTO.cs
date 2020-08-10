using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.DTO
{
    public class CandidatoEleccionesDTO
    {

        public string Nombre { get; set; }

        public List<CandidatoEleccionEspecificaDTO> ListadoCandidatos { get; set; }

    }

}
