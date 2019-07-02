using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class CicloViewModel
    {
        public List <Modalidad> modalidadList { get; set; }
        [Required]
        public int? ModalidadId { get; set; }
        public int Id { get; set; }

        
        public DateTime? fecha_inicio { get; set; }
        
        public DateTime? fecha_fin { get; set; }

        public CicloViewModel() {
            Id = 0;
        }
        public CicloViewModel(Ciclo c) {
            fecha_inicio = c.fecha_inicio;
            fecha_fin = c.fecha_fin;
            ModalidadId = c.ModalidadId;
        }
    }
}