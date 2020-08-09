using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage ="Este Campo Usuario es requerido")]
        [Remote(action: "VerifyCedula", controller: "Start")]

        public string Cedula { get; set; }
 
    }
}
