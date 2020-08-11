using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.DTO
{
    public class EleccionesResulDTO
    {
        public int IdEleccion { get; set; }
        [Required(ErrorMessage = "El nombre de la eleccion es requerido")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Debe elegir la fecha de la Elecciones")]
        //[Remote(action: "Verifydate", controller: "Elecciones")]
        public DateTime Fecha { get; set; }

        public string Estado { get; set; }
        public bool Procesoactivos { get; set; }
        public bool disponibilidadecandidatos { get; set; }
        public bool disponibilidadpuestos { get; set; }


        public List<List<ResultadosDTO>> Resultados { get; set; }
        public IEnumerable<EleccionesDTO> elecciones { get; set; }
    }
}
