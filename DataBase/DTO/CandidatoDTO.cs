using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DataBase.DTO
{
    public class CandidatoDTO
    {

        public int IdCandidato { get; set; }

        [Required(ErrorMessage = "El nombre del candidato debe ser llenado")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido del candidato debe ser llenado")]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Id del partido debe ser insertado")]
        public int IdPartido { get; set; }
        
        [Required(ErrorMessage = "El Id del puesto electivo debe ser insertado")]
        public int IdPuestoElectivo { get; set; }
        
        public string FotoPerfil { get; set; }

        public string Estado { get; set; }

        public string Puesto { get; set; }

    }

   

}
