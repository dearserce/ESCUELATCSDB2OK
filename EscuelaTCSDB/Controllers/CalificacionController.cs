using EscuelaTCSDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EscuelaTCSDB.Models.ViewModel;
using Microsoft.AspNet.Identity;

namespace EscuelaTCSDB.Controllers
{
    [Authorize(Roles = "Directivo , Profesor")]
    public class CalificacionController : Controller
    {
        public ApplicationDbContext _ctx;

        private int id_profe { get {
                string id_str = User.Identity.GetUserId();
                var todo = _ctx.Users.Where(x => x.Id == id_str).FirstOrDefault();
                int id_bien = todo.PersonaId.Value;
                return id_bien;
            } }
        public CalificacionController()
        {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
    
        public ActionResult Index()
        {
            try
            {
                User.Identity.GetUserId();
                List<GrupoPersona> gruposPersona = this.obtenerGrupos(id_profe);
                List<Grupo> grupos = new List<Grupo>();
                for (int i = 0; i < gruposPersona.Count; i++)
                {
                    Grupo g = gruposPersona[i].Grupo;
                    grupos.Add(g);
                }

                //ahora removemos elementos repetidos.
                List<Grupo> gruposFiltrados = grupos.Distinct().ToList();
                ViewBag.ListaGrupos = new SelectList(gruposFiltrados, "id", "codigo");
                return View();
            }
            catch (Exception e) {
                return View();
            }
        }
        //Agregar Calificaciones
        public ActionResult AgregarCalificaciones(int id_gpp, double calActual, double calNueva) {
            try {
                //primero buscamos a ver si ya existe la calificacion.
                Calificacion c = _ctx.Calificaciones.Where(x => x.GpPeriodoId == id_gpp).FirstOrDefault();
                GpPeriodo gp = _ctx.GpPeriodos.Where(x => x.Id == id_gpp)
                    .Include(x => x.GrupoPersona).FirstOrDefault();
                if (c != null)
                {
                    //Es edicion
                    c.calificacion = calNueva;
                    _ctx.SaveChanges();
                }
                else {
                    //Es asignacion nueva
                    c = new Calificacion();
                    c.GpPeriodo = gp;
                    c.GpPeriodoId = id_gpp;
                    c.calificacion = calNueva;

                    _ctx.Calificaciones.Add(c);
                    _ctx.SaveChanges();
                }
                return Filtrar(gp.GrupoPersonaId,gp.GrupoPersona.CicloId,gp.periodo,gp.GrupoPersona.MateriaId);
            } catch (Exception e) {
                return View("Index");
            }
        }
        //Filtros
        public ActionResult Filtrar(int id_grupo, int id_ciclo,int periodo, int id_materia) {
            try {
                List<GrupoPersona> gruposPersona = obtenerGrupos(id_profe);
                var viewModel =
                    (from gp in gruposPersona
                     join gpp in _ctx.GpPeriodos on gp.Id equals gpp.GrupoPersonaId
                     join calif in _ctx.Calificaciones on gpp.Id equals calif.GpPeriodoId into califs
                     from x in califs.DefaultIfEmpty()
                     where gp.GrupoId == id_grupo
                     && gp.CicloId == id_ciclo
                     && gp.MateriaId == id_materia
                     && gpp.periodo == periodo
                     select new CalificacionesViewModel {
                         Id =gpp.Id,
                         Nombre = gp.Persona.nombre +" "+ gp.Persona.apellido,
                         Calificacion = _ctx.Calificaciones.SingleOrDefault(x => x.GpPeriodoId == gpp.Id) != null ? _ctx.Calificaciones.SingleOrDefault(x => x.GpPeriodoId == gpp.Id).calificacion : 0
                     }).ToList();

                //ahora armamos lo que se va a ver
                

                return View("ListaCalificaciones", viewModel);
            }
            catch (Exception e) {
                return View("Index");
                }
        }

        //obtiene grupos
        public List<GrupoPersona> obtenerGrupos(int id_profesor) {
            List<GrupoPersona> gruposPersona =
                _ctx.GrupoPersonas
                .Include(x => x.Grupo)
                .Include(x => x.Ciclo)
                .Include(x => x.Materia)
                .Include(x => x.Ciclo.Modalidad)
                .Include(x => x.Persona).
                Where(x => x.ProfesorId == id_profesor).ToList();
            return gruposPersona;
        }
        public ActionResult obtenerPeriodos(int id_ciclo, int id_grupo, int id_materia) {
            //cuando seleccionen el ciclo, los periodos se van a llenar en un dropdownlist
            List<GrupoPersona> gruposPersona = obtenerGrupos(id_profe);
            List<GrupoPersona> gruposFiltrado = gruposPersona.Where(x => x.Grupo.Id == id_grupo && x.Ciclo.Id == id_ciclo && x.Materia.Id == id_materia).ToList();
            List<GrupoPersona> gruposDistinct = gruposFiltrado.Distinct().ToList();
            List<int> ids_grupos = new List<int>();
            for (int i = 0; i < gruposDistinct.Count; i++) {
                int id = gruposDistinct[i].Id;
                ids_grupos.Add(id);
            }
            List<GpPeriodo> gp_periodos = _ctx.GpPeriodos.Where(x => ids_grupos.Contains(x.GrupoPersona.Id)).ToList();
            List<GpPeriodo> gpf = gp_periodos.Distinct().ToList();
            List<int> periodos_enteros = new List<int>();
            for (int i = 0; i < gpf.Count; i++) {
                int periodo = gpf[i].periodo;
                periodos_enteros.Add(periodo);
            }
            List<int> periodos_final = periodos_enteros.Distinct().ToList();
            List<DDLPeriodosViewModel> ddlpvm = new List<DDLPeriodosViewModel>();
            for (int i = 0; i < periodos_final.Count; i++) {
                DDLPeriodosViewModel dl = new DDLPeriodosViewModel();
                dl.periodo = periodos_final[i];
                dl.descripcion = "Periodo " + periodos_final[i];
                ddlpvm.Add(dl);
            }
         
            return View("DDLPeriodos",ddlpvm);
        }
        //obtiene ciclos, es ActionResult por que son llamadas por Ajax.
      
        public ActionResult obtenerCiclos(int id_grupo)
        {
            List<GrupoPersona> gruposPersona = this.obtenerGrupos(id_profe);
            List<GrupoPersona> gruposFiltrado = gruposPersona.Where(x => x.Grupo.Id == id_grupo).ToList();
            List<Ciclo> ciclos = new List<Ciclo>();
            for (int i = 0; i < gruposFiltrado.Count; i++) {
                Ciclo c = gruposFiltrado[i].Ciclo;
                ciclos.Add(c);
            }
            //se remueven los valores repetidos.
            List<Ciclo> ciclosFiltrados = ciclos.Distinct().ToList();
            return View("DDLCiclos", ciclosFiltrados);
        }

        //Ahora Se filtran las Materias.
        public ActionResult obtenerMaterias(int id_ciclo, int id_grupo) {
            List<GrupoPersona> gruposPersona = this.obtenerGrupos(id_profe);
            List<GrupoPersona> gruposPersonaFiltrado =
                gruposPersona.Where(x => x.Grupo.Id == id_grupo && x.Ciclo.Id  == id_ciclo).ToList();
            List<Materia> materias = new List<Materia>();
            for (int i = 0; i < gruposPersonaFiltrado.Count; i++) {
                Materia m = gruposPersonaFiltrado[i].Materia;
                materias.Add(m);
            }
            List<Materia> sinRepetirMaterias = materias.Distinct().ToList();
            return View("DDLMateria",sinRepetirMaterias);
        }

    }
}