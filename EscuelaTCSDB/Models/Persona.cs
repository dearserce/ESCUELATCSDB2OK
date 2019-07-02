using EscuelaTCSDB.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models
{
    public class Persona
    {
        [Required]
       public int TipoPersonaId { get; set; }
        public TipoPersona TipoPersona { get; set; }
        public int Id { get; set; }
        [StringLength(100)]
        [Index(IsUnique = true)]
        [EmailAddress]
        [Required]
        public string email { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        [Required]
        public string password { get; set; }
        public List<ApplicationUser> usuarios { get; set; }
        public static Persona GetByEmail(String email, int id)
        {
            Persona persona = null;
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                persona = _context.Personas.SingleOrDefault(x => x.email == email && x.Id != id);

            }
            return persona;
        }
    }
}