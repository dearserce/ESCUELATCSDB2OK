using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models
{
    public class GrupoPersona
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Grupo")]
        public int GrupoId { get; set; }
        [Required]
        [Display(Name = "Materia")]
        public int MateriaId { get; set; }
        [Required]
        [Display(Name = "Ciclo")]
        public int CicloId { get; set; }
        [Required]
        public int PersonaId { get; set; }
        [Required]
        public int ProfesorId { get; set; }

        
        [Display(Name = "Grupo")]
        public Grupo Grupo { get; set; }
        [Display(Name = "Materia")]
        public Materia Materia { get; set; }
        [Display(Name = "Ciclo")]
        public Ciclo Ciclo { get; set; }
        [Display(Name = "Persona")]
        public Persona Persona { get; set; }
        [Display(Name = "Profesor")]
        public Persona Profesor { get; set; }
    }
}