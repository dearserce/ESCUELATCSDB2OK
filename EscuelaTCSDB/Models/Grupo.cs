using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        [Index(IsUnique=true)]
        [StringLength(30)]
        [Required]
        public string codigo { get; set; }
        public static Grupo GetByCodigo(string codigo, int id) {
            Grupo grupo = null;
            using (ApplicationDbContext _ctx = new ApplicationDbContext()) {
                grupo = _ctx.Grupos.SingleOrDefault(x => x.codigo == codigo && x.Id != id);
            }
            return grupo;
        }
    }
}