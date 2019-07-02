using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class AlumnosCalificacionViewModel
    {
        public List<CalificacionTableViewModel> Calificaciones { get; set; }
        public List<CalificacionColumna> Columnas { get; set; }
    }
}