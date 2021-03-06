﻿using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Partidos
    {
        public Partidos()
        {
            Candidatos = new HashSet<Candidatos>();
        }

        public int IdPartido { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Logo { get; set; }
        public string Estado { get; set; }

        public ICollection<Candidatos> Candidatos { get; set; }
    }
}
