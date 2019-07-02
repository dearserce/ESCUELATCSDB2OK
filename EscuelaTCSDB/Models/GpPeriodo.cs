using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models
{
    public class GpPeriodo
    {
        public int Id { get; set; }
        [Required]
        public int GrupoPersonaId { get; set; }
        public GrupoPersona GrupoPersona { get; set; }
        [Required]
        public int periodo { get; set; }
    }
}