using DataBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DataBase.DTO
{
    public class CrearPuestoElectivoDTO
    {
        
        public int IdPuestoElectivo { get; set; }

        [PuestoNombre(ErrorMessage = "Este Puesto Electivo ya existe!")]
        [Required(ErrorMessage = "El nombre del puesto electivo debe ser llenado")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El nombre del puesto electivo debe ser llenado")]
        public string Descripcion { get; set; }

        public string Estado { get; set; }


    }

    public class PuestoNombreAttribute : ValidationAttribute
    {


        public override bool IsValid(object value)
        {

            ItlaElectorDBContext context = new ItlaElectorDBContext();

            var ListUsuario = context.PuestoElectivo.Select(x => x.Nombre).ToList();

            if (ListUsuario.Contains(value))
            {
                return false;
            }

            return true;
        }

    }


}
