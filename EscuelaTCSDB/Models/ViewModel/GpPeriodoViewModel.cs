using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class GpPeriodoViewModel
    {
        public List<GrupoPersona> grupoPersonaList { get; set; }
        public int Id { get; set; }
        [Required]
        public int GrupoPersonaId { get; set; }
        public GrupoPersona GrupoPersona { get; set; }
        [Required]
        public int periodo { get; set; }
    }
}