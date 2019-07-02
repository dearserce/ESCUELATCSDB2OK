using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EscuelaTCSDB.Models.ViewModel
{
    public class GrupoPersonaViewModel
    {
        public List <Grupo> grupoList { get; set; }
        public List <Materia> materiaList { get; set; }
        public List <Ciclo> cicloList { get; set; }
        public List <Persona> personaList { get; set; }
        public List <Persona> profesorList { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo Grupo es obligatorio")]
        public int GrupoId{ get; set; }
        [Required(ErrorMessage = "Debes de seleccionar una Materia")]
        public int MateriaId { get; set; }
        [Required(ErrorMessage = "Debes de seleccionar un Ciclo")]
        public int CicloId{ get; set; }
        [Required(ErrorMessage = "Debes de seleccionar una Persona")]
        public int PersonaId { get; set; }
        [Required(ErrorMessage = "Debes de seleccionar un Profesor")]
        public int ProfesorId { get; set; }

    
        public GrupoPersonaViewModel() {
            Id = 0;
        }
        public GrupoPersonaViewModel(GrupoPersona gp) {
            GrupoId = gp.Grupo.Id;
            MateriaId = gp.Materia.Id;
            CicloId = gp.Ciclo.Id;
            PersonaId = gp.Persona.Id;
            ProfesorId = gp.Profesor.Id;
        }
    }
}