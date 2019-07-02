using EscuelaTCSDB.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models
{
    public class TipoPersona
    {
        //Generamos valores constantes para los id.
        public const int Alumno = 1;
        public const int Profesor = 2;
        public const int Directivo = 3;

        public int Id { get; set; }
        [StringLength(100)]
        [Index(IsUnique = true)]
        [TipoPersonaValidation]
        [Required]
        public string descripcion { get; set; }

        public static TipoPersona GetByDescripcion(String descripcion, int id)
        {
            TipoPersona tipoPersona = null;
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                tipoPersona = _context.TipoPersonas.SingleOrDefault(x => x.descripcion == descripcion && x.Id != id);

            }
            return tipoPersona;
        }
    
      
    }
}