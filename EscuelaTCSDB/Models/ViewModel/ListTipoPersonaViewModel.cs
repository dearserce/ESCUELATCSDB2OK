using EscuelaTCSDB.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class ListTipoPersonaViewModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Index(IsUnique = true)]
        [TipoPersonaValidation]
        [Required]
        public string descripcion { get; set; }

        public ListTipoPersonaViewModel()
        {
            Id = 0;
        }

        public ListTipoPersonaViewModel(TipoPersona t) {
            descripcion = t.descripcion;
            Id = t.Id;
        }

    }
}