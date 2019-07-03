using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class CalificacionTableViewModel
    {
        public int GpPeriodoId { get; set; }
        public int Periodo { get; set; }
        public double? Calificacion { get; set; }
        public string Materia { get; set; }
        public int MateriaId { get; set; }
    }
}