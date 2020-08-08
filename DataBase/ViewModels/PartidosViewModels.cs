using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.ViewModels
{
    public class PartidosViewModels
    {
        public int IdPartido { get; set; }
        [Required(ErrorMessage = "El partido debe tener un nombre")]

        public string Nombre { get; set; }

        [Required(ErrorMessage = "El partido debe tener una descripcion")]

        public string Descripcion { get; set; }
        public string Logo { get; set; }
        public string Estado { get; set; }
        [Required(ErrorMessage = "El candidato debe identificarse con una foto")]
        public IFormFile Photo { get; set; }
        public IEnumerable<PartidosViewModels> partidos { get; set; }
    }
}
