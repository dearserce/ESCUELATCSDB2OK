using EscuelaTCSDB.Models;
using EscuelaTCSDB.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EscuelaTCSDB.Controllers
{
    public class GrupoController : Controller
    {
        // GET: Grupo
        public ApplicationDbContext _ctx;

        public GrupoController() {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }

        public ActionResult Index()
        {
            List<Grupo> gl = _ctx.Grupos.ToList();
            return View(gl);
        }
        public ActionResult Create() {
            GrupoViewModel vm = new GrupoViewModel();
            return View("FormGrupo", vm);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Crear(GrupoViewModel gvm) {
            try {
                if (!ModelState.IsValid) {
                    //Algo estuvo mal.
                    return View("FormGrupo");
                }
                if (gvm.Id == 0)
                {
                    Grupo g = new Grupo();
                    g.codigo = gvm.codigo;
                    _ctx.Grupos.Add(g);
                }
                else {
                    //Estan editando
                    var grupoeExistente = _ctx.Grupos.SingleOrDefault(x => x.Id == gvm.Id);
                    if (grupoeExistente != null) {
                        grupoeExistente.codigo = gvm.codigo;
                    }
                }
            } catch {
                return View();
            }
            _ctx.SaveChanges();
            return RedirectToAction("Index");
               
        }

        //Editar
        public ActionResult Editar(int id) {
            var grupo = _ctx.Grupos.Find(id);
            GrupoViewModel gvm = new GrupoViewModel(grupo);
            return View("FormGrupo", gvm);
        }
        //eliminar
        public ActionResult Eliminar(int id) {
            var grupo = _ctx.Grupos.Find(id);
            _ctx.Grupos.Remove(grupo);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}