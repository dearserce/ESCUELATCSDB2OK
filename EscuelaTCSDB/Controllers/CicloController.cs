using EscuelaTCSDB.Models;
using EscuelaTCSDB.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace EscuelaTCSDB.Controllers
{
    [Authorize(Roles = "Directivo")]
    public class CicloController : Controller
    {
       
        public ApplicationDbContext _ctx;

        public CicloController()
        {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }

        public ActionResult Index()
        {
            List<Ciclo> gl = _ctx.Ciclos.Include(x => x.Modalidad).ToList();
            return View(gl);
        }
        public ActionResult Create() {
            List<Modalidad> modalidades = _ctx.Modalidades.ToList();
            CicloViewModel cvm = new CicloViewModel();
            cvm.modalidadList = modalidades;
            cvm.fecha_inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            cvm.fecha_fin= new DateTime(DateTime.Now.Year+1, DateTime.Now.Month, DateTime.Now.Day);

            return View("FormCiclo", cvm);
        }
        public ActionResult Crear(CicloViewModel cvm) {
            try
            {
                if (!ModelState.IsValid)
                {
                    /*  pvm.TipoPersonas = _ctx.TipoPersonas.ToList();
                    return View("FormPersona", pvm);*/
                    cvm.modalidadList = _ctx.Modalidades.ToList();
                    //Algo estuvo mal.
                    return View("FormCiclo",cvm);
                }
                if (cvm.Id == 0)
                {
                    Ciclo c = new Ciclo();
                    c.fecha_inicio = cvm.fecha_inicio;
                    c.fecha_fin = cvm.fecha_fin;
                    c.ModalidadId = cvm.ModalidadId.Value;
                    _ctx.Ciclos.Add(c);
                }
                else
                {
                    //Estan editando
                    var cicloExistente = _ctx.Ciclos.SingleOrDefault(x => x.Id == cvm.Id);
                    if (cicloExistente != null)
                    {
                        cicloExistente.fecha_inicio = cvm.fecha_inicio;
                        cicloExistente.fecha_fin = cvm.fecha_fin;
                        cicloExistente.ModalidadId = cvm.ModalidadId.Value;

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
            var ciclo = _ctx.Ciclos.Find(id);
            CicloViewModel cvm = new CicloViewModel(ciclo);
            return View("FormCiclo", cvm);
        }
        //eliminar
        public ActionResult Eliminar(int id)
        {
            var ciclo = _ctx.Ciclos.Find(id);
            _ctx.Ciclos.Remove(ciclo);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}