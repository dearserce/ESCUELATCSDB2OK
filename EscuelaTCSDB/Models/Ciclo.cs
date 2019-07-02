using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models
{
    public class Ciclo
    {
        [Required]
        public int ModalidadId { get; set; }
        public Modalidad Modalidad { get; set; }
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? fecha_inicio { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? fecha_fin { get; set; }

        [NotMapped]
        public string Descripcion
        {
            get
            {
                string fechaInicio = this.fecha_inicio != null ? this.fecha_inicio.Value.ToString("dd/MM/yyyy") : "nachos";
                string fechaFin = this.fecha_fin != null ? this.fecha_fin.Value.ToString("dd/MM/yyyy") : "nachos";
                return fechaInicio + " -" + fechaFin;
            }
        }
    }
}