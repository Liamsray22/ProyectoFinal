using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.ViewModels
{
    public class LoginUserViewModel
    {
        [StringLength(13, ErrorMessage = "Siga el formato dado")]
        [Required(ErrorMessage ="Para acceder a la zona de votación debes colocar tu cédula.")]
        [Remote(action: "VerifyCedula", controller: "Start")]
        public string Cedula { get; set; }
    }
}
