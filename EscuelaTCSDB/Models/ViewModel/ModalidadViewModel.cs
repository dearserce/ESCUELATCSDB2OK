using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class ModalidadViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        [Index(IsUnique = true)]
        public string descripcion { get; set; }
        [Required]
        [Display(Name = "Cantidad de meses")]
        public int n_meses { get; set; }
        [Required]
        [Display(Name = "Cantidad de periodos")]
        public int n_periodos { get; set; }

        public ModalidadViewModel() {
            Id = 0;
        }
        public ModalidadViewModel(Modalidad p) {
            descripcion = p.descripcion;
            n_meses = p.n_meses;
            n_periodos = p.n_periodos;
        }
        
    }
}