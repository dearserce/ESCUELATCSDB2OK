using EscuelaTCSDB.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using EscuelaTCSDB.Models.ViewModel;

namespace EscuelaTCSDB.Controllers
{
    [Authorize(Roles = "Alumno")]
    public class AlumnoController : Controller
    {
        public int id_alumno { get {
                int id_int = 0;
                try {
                    string id_str = User.Identity.GetUserId();
                    var persona = _ctx.Users.Where(x => x.Id == id_str).FirstOrDefault();
                    id_int = persona.PersonaId.Value;
                    return id_int;
                } catch {
                    return id_int;
                }
            } } //paco 

        // GET: Alumno
        public ApplicationDbContext _ctx;
        public AlumnoController()
        {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }

        public ActionResult Index()
        {
            AlumnoViewModel avm = new AlumnoViewModel();
            List<Materia> Materias =
                _ctx.Materias
                .Where(x => _ctx.GrupoPersonas.Any(y => y.MateriaId == x.Id && y.PersonaId == id_alumno)).ToList();
            avm.Materias = Materias;
            avm.id_persona = id_alumno;
            return View(avm); 
        }
        
    }
}