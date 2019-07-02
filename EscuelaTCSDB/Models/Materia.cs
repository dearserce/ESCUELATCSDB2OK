using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models
{
    public class Materia
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        [Index(IsUnique = true)]
        public string nombre { get; set; }
        [StringLength(255)]
        public string descripcion { get; set; }
        [Required]
        public bool activo { get; set; }

        public static Materia GetByNombre(String nombre, int id)
        {
            Materia m = null;
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                m = _context.Materias.SingleOrDefault(x => x.nombre == nombre && x.Id != id);

            }
            return m;
        }
    }
}