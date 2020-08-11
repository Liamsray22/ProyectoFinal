using DataBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DataBase.DTO
{
    public class ResultadosDTO
    {
        public int ideleccion { get; set; }

        public string Nombre { get; set; }

        public string Puesto { get; set; }

        public int Votos { get; set; }

        public double porcentaje { get; set; }
    }
    }
