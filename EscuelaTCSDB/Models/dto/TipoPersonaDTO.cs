using EscuelaTCSDB.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models
{
    public class TipoPersonaDTO
    {
        public int Id { get; set; }
        [Required]
        public string descripcion { get; set; }
    }
}