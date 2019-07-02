using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models
{
    public class Modalidad
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        [Index(IsUnique=true)]
        public string descripcion { get; set; }
        [Required]
        public int n_meses { get; set; }
        [Required]
        public int n_periodos { get; set; }
      
        public static Modalidad GetByDescripcion(string descripcion, int id) {
            Modalidad modalidad = null;
            using (ApplicationDbContext _ctx = new ApplicationDbContext()) {
                modalidad = _ctx.Modalidades.SingleOrDefault(x => x.descripcion == descripcion && x.Id != id);
            }
            return modalidad;
        }
    }
}