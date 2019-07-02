using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models
{
    public class Calificacion
    {
        public int Id { get; set; }
        [Required]
        public int GpPeriodoId { get; set; }
        public GpPeriodo GpPeriodo{ get; set; }
        public double calificacion { get; set; }
        public static Calificacion GetByGppCalifVieja(int id_gpp, double calif)
        {
            Calificacion ca = null;
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                ca  = _context.Calificaciones.SingleOrDefault(x => x.GpPeriodoId == id_gpp && x.calificacion == calif);
            }
            return ca;
        }
    }
}