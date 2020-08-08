using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Este Campo Usuario es requerido")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "El Campo Clave es requerido")]
        public string Clave { get; set; }
    }
}
