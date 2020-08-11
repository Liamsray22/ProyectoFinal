using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.ViewModels
{
    public class PartidosViewModels
    {
        public int IdPartido { get; set; }
        [Required(ErrorMessage = "Debe ingresar nombre de partido")]
        [Remote(action: "VerifyPartido", controller: "Partidos")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe ingresar descripcion de partido")]

        public string Descripcion { get; set; }
        public string Logo { get; set; }
        public string Estado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una foto")]
        public string newfoto { get; set; }
        public IFormFile Photo { get; set; }
        public IEnumerable<PartidosViewModels> partidos { get; set; }
    }
}
