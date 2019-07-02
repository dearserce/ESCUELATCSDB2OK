using System;
using System.Collections.Generic;
using EscuelaTCSDB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EscuelaTCSDB.Models.ViewModel;

namespace EscuelaTCSDB.Controllers
{
    //validacion
    [Authorize(Roles = "Directivo")]
    public class TipoPersonaController : Controller
    {
        // GET: TipoPersona
        private ApplicationDbContext _ctx;

        public TipoPersonaController() {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }

        public ActionResult Index()
        {
            
            List<TipoPersona> ltp = _ctx.TipoPersonas.ToList();
            return View(ltp);
        }

        public ActionResult Create() {
            ListTipoPersonaViewModel vm = new ListTipoPersonaViewModel();
            return View("FormTipoPersona", vm);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Crear(TipoPersona tp) {
            try {
                if (!ModelState.IsValid) {
                    ListTipoPersonaViewModel vm = new ListTipoPersonaViewModel(tp);
                    return View("FormTipoPersona", vm);
                }
                if (tp.Id == 0) {
                    _ctx.TipoPersonas.Add(tp);
                }
                else
                {
                    var valor = _ctx.TipoPersonas.SingleOrDefault(
                        m => m.Id == tp.Id
                        );
                    if (valor != null) {
                        valor.descripcion = tp.descripcion;
                    }
                }
            } catch {
                return View();
            }

            //aqui llegamos siempre
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        //Editar
        public ActionResult Editar(int id)
        {
            var tipo = _ctx.TipoPersonas.Find(id);
            ListTipoPersonaViewModel vm = new
              ListTipoPersonaViewModel  (tipo);

            return View("FormTipoPersona",vm);
        }
        //Eliminar
        public ActionResult Eliminar(int id)
        {
            var tipo = _ctx.TipoPersonas.Find(id);
            _ctx.TipoPersonas.Remove(tipo);
            _ctx.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}