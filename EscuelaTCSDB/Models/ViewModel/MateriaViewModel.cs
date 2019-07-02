using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class MateriaViewModel
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

        public MateriaViewModel() {
            Id = 0;
        }

        public MateriaViewModel(Materia m) {
            Id = m.Id;
            nombre = m.nombre;
            descripcion = m.descripcion;
            activo = m.activo;
        }
        
    }
}