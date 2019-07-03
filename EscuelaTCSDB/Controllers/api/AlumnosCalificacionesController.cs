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
            //List<CalificacionTableViewModel> calificaciones = (from c in _ctx.Calificaciones
            //                                     .Include(x => x.GpPeriodo.GrupoPersona.Materia)
            //                                                   where _ctx.GpPeriodos.Any(y => y.Id == c.GpPeriodoId
            //                                                                               && y.GrupoPersona.PersonaId == id_alumno)
            //                                                   select new CalificacionTableViewModel
            //                                                   {
            //                                                       Periodo = c.GpPeriodo.periodo,
            //                                                       Calificacion = c.calificacion,
            //                                                       Materia = c.GpPeriodo.GrupoPersona.Materia.nombre,
            //                                                       MateriaId = c.GpPeriodo.GrupoPersona.MateriaId
            //                                                   }).ToList();

            List<CalificacionTableViewModel> calificaciones = (from gpp in _ctx.GpPeriodos.Include(x => x.GrupoPersona.Materia)
                                                                join c in _ctx.Calificaciones on gpp.Id equals c.GpPeriodoId into califs
                                                                from x in califs.DefaultIfEmpty()
                                                                where gpp.GrupoPersona.PersonaId == id_alumno
                                                                select new CalificacionTableViewModel
                                                                {
                                                                    GpPeriodoId = gpp.Id,
                                                                    Periodo = gpp.periodo,
                                                                    Calificacion = x.calificacion,
                                                                    Materia = gpp.GrupoPersona.Materia.nombre,
                                                                    MateriaId = gpp.GrupoPersona.MateriaId
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
                foreach (var calif in calificaciones.Where(x => x.Materia == mat).OrderBy(x => x.Periodo))
                {
                    var gpp = _ctx.GpPeriodos.Where(x => x.periodo == calif.Periodo && x.GrupoPersona.MateriaId == calif.MateriaId && x.GrupoPersona.PersonaId == id_alumno).FirstOrDefault();
                    if(gpp != null)
                    {
                        obj.Add("Id_" + calif.Periodo, gpp.Id);                        
                    }
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
        //cuando el mono actualiza su calificacion porque
        [HttpPut]
        public IHttpActionResult Put(int id_gpp, double calificacion) {
            try {
                Calificacion c = null;
                c = _ctx.Calificaciones.Where(x => x.GpPeriodoId == id_gpp).FirstOrDefault();
                if (c != null)
                {
                    c.calificacion = calificacion;
                    _ctx.SaveChanges();
                }
                else {
                    c = new Calificacion();
                    
                    c.calificacion = calificacion;
                    c.GpPeriodoId = id_gpp;

                    _ctx.Calificaciones.Add(c);
                    _ctx.SaveChanges();

                }
                return Ok(c);
            } catch (Exception e) {
                return BadRequest();
            }
        }

    }
}
