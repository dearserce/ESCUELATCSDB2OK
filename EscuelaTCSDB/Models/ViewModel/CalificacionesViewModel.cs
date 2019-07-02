using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class CalificacionesViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Calificacion { get; set; }
    }
}