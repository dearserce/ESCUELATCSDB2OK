using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class GrupoViewModel
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [StringLength(30)]
        [Required]
        public string codigo { get; set; }

        public GrupoViewModel() {
            Id = 0;
        }

        public GrupoViewModel(Grupo g) {
            Id = g.Id;
            codigo = g.codigo;
        }
    }
}