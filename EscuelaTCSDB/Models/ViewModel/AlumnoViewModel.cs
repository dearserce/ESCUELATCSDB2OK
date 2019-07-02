using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class AlumnoViewModel
    {
        public List<Materia> Materias { get; set; }
        public int id_persona { get; set; }
    }
}