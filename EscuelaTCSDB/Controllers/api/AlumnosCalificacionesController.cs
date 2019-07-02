using EscuelaTCSDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using EscuelaTCSDB.Models.ViewModel;
using System.Dynamic;

namespace EscuelaTCSDB.Controllers.api
{
    public class AlumnosCalificacionesController : ApiController
    {
        public ApplicationDbContext _ctx;

        public AlumnosCalificacionesController()
        {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
        [HttpGet]
        public IHttpActionResult Get(int id_alumno)
        {
            List<CalificacionTableViewModel> calificaciones = (from c in _ctx.Calificaciones
                                                 .Include(x => x.GpPeriodo.GrupoPersona.Materia)
                                                               where _ctx.GpPeriodos.Any(y => y.Id == c.GpPeriodoId
                                                                                           && y.GrupoPersona.PersonaId == id_alumno)
                                                               select new CalificacionTableViewModel
                                                               {
                                                                   Periodo = c.GpPeriodo.periodo,
                                                                   Calificacion = c.calificacion,
                                                                   Materia = c.GpPeriodo.GrupoPersona.Materia.nombre,
                                                                   MateriaId = c.GpPeriodo.GrupoPersona.MateriaId
                                                               }).ToList();

            if (calificaciones == null)
            {
                return NotFound();
            }

            //LLENAR COLUMNAS
            List<CalificacionColumna> lcctvm = new List<CalificacionColumna>();
            CalificacionColumna colMateria = new CalificacionColumna() { data = "materia", name = "Materia" };
            lcctvm.Add(colMateria);

            var periodosDistinct = calificaciones.Select(x => x.Periodo).Distinct().Count();

            for (int i = 0; i < periodosDistinct; i++)
            {
                string periodo = calificaciones[i].Periodo.ToString();
                CalificacionColumna cctvm = new CalificacionColumna();
                cctvm.data = periodo;
                cctvm.name = "P_" + periodo;
                lcctvm.Add(cctvm);
            }

            List<Object> calificacionesObjects = new List<object>();
            foreach(var  mat in calificaciones.Select(x => x.Materia).Distinct())
            {
                dynamic expando = new ExpandoObject();
                var obj = expando as IDictionary<string, object>;
                obj.Add(colMateria.data, mat);
                foreach(var calif in calificaciones.Where(x => x.Materia ==mat).OrderBy(x => x.Periodo))
                {
                    obj.Add(calif.Periodo.ToString(), calif.Calificacion);
                }                
                calificacionesObjects.Add(obj);
            }

            //AlumnosCalificacionViewModel acvm = new AlumnosCalificacionViewModel()
            //{
            //    Calificaciones = calificaciones,
            //    Columnas = lcctvm
            //};
            //acvm.Calificaciones.OrderBy(x => x.Periodo);
            //acvm.Columnas.OrderBy(x => x.Periodo);

            var result = new {
                data = calificacionesObjects,
                columns = lcctvm
            };

            return Ok(result);
        }
    }
}
