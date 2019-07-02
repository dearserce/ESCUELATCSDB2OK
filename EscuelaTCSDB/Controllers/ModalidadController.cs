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
    public class ModalidadController : Controller
    {
        // GET: Modalidad
        public ApplicationDbContext _ctx;
        public ModalidadController() {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
        public ActionResult Index()
        {
            List<Modalidad> lm = _ctx.Modalidades.ToList();
            return View(lm);
        }
        public ActionResult Create() {
            ModalidadViewModel vm = new ModalidadViewModel();
            return View("FormModalidad", vm);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Crear(ModalidadViewModel mvm) {
            try
            {
                if (!ModelState.IsValid)
                {
                    //Algo estuvo mal.
                    return View("FormModalidad");
                }
                if (mvm.Id == 0)
                {
                    Modalidad m = new Modalidad();
                    m.descripcion = mvm.descripcion;
                    m.n_meses = mvm.n_meses;
                    m.n_periodos = mvm.n_periodos;
                    _ctx.Modalidades.Add(m);
                }
                else
                {
                    //Estan editando
                    var modalidadExistente = _ctx.Modalidades.SingleOrDefault(x => x.Id == mvm.Id);
                    if (modalidadExistente != null)
                    {
                        modalidadExistente.descripcion = mvm.descripcion;
                        modalidadExistente.n_meses = mvm.n_meses;
                        modalidadExistente.n_periodos = mvm.n_periodos;
                    }
                }
            }
            catch
            {
                return View();
            }
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
        //Editar
        public ActionResult Editar(int id)
        {
            var modalidad = _ctx.Modalidades.Find(id);
            ModalidadViewModel mvm = new ModalidadViewModel(modalidad);
            return View("FormModalidad", mvm);
        }
        //eliminar
        public ActionResult Eliminar(int id)
        {
            var modalidad = _ctx.Modalidades.Find(id);
            _ctx.Modalidades.Remove(modalidad);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}