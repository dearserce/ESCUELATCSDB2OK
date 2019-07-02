using EscuelaTCSDB.Models;
using EscuelaTCSDB.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscuelaTCSDB.Controllers
{
    [Authorize(Roles = "Directivo")]
    public class MateriaController : Controller
    {
        // GET: Materias
        private ApplicationDbContext _ctx;

        public MateriaController()
        {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
        public ActionResult Index()
        {
            List<Materia> lm = _ctx.Materias.ToList();
            return View(lm);
        }

        public ActionResult Create() {
            MateriaViewModel mvm = new MateriaViewModel();
            return View("FormMateria", mvm);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Crear(MateriaViewModel mvm) {
            try {
                if (!ModelState.IsValid) {
                    return View("FormMateria");
                }

                if (mvm.Id == 0)
                {
                    Materia m = new Materia();
                    m.nombre = mvm.nombre;
                    m.descripcion = mvm.descripcion;
                    m.activo = mvm.activo;
                    _ctx.Materias.Add(m);
                }
                else {
                    //Es una edicion
                    var valor = _ctx.Materias.SingleOrDefault(m => m.Id == mvm.Id);
                    if (valor != null) {
                        valor.nombre = mvm.nombre;
                        valor.descripcion = mvm.descripcion;
                        valor.activo = mvm.activo;

                    }
                }
            } catch {
                return View();
            }
            //Aqui se llega sIempre
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id) {
            var materia = _ctx.Materias.Find(id);
            MateriaViewModel mvm = new MateriaViewModel(materia);
            return View("FormMateria", mvm);
        }
        public ActionResult Eliminar(int id) {
            var materia = _ctx.Materias.Find(id);
            _ctx.Materias.Remove(materia);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}