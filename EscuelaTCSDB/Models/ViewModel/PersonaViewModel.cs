using EscuelaTCSDB.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class PersonaViewModel
    {
        public List<TipoPersona> TipoPersonas { get; set; }
        [Required]
        public int? TipoPersonaId { get; set; }
        public int Id { get; set; }
        [StringLength(100)]
        [Index(IsUnique = true)]
        [PersonaEmailValidation]
        [EmailAddress] 
        [Required]
        public string email { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        [Required]
        public string password { get; set; }

        public PersonaViewModel()
        {
            Id = 0;
        }

        public PersonaViewModel(Persona p)
        {
            Id = p.Id;
            nombre = p.nombre;
            apellido = p.apellido;
            password = p.password;
            email = p.email;
            TipoPersonaId = p.TipoPersonaId;
        }
    }
}