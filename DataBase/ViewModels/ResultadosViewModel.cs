using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DataBase.ViewModels
{
    public class ResultadosViewModel
    {

        public int ideleccion { get; set; }

        public string Nombre { get; set; }

        public string Puesto { get; set; }

        public int Votos { get; set; }

        public int porcentaje { get; set; }
       
    }
}
