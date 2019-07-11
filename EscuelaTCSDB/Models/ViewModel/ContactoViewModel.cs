using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class ContactoViewModel
    {
        [Required]
        public string Nombre{ get; set; }
        public string Correo { get; set; }
        [Required]
        public string Comentario { get; set; }
    }
}