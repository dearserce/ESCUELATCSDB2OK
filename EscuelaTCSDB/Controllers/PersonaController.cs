using EscuelaTCSDB.Models;
using EscuelaTCSDB.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;

namespace EscuelaTCSDB.Controllers
{
    [Authorize(Roles = "Directivo")]
    public class PersonaController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationDbContext _ctx;

        public PersonaController()
        {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
        public ActionResult Index()
        {
            //            List<TipoPersona> ltp = _ctx.TipoPersonas.ToList();
            List<Persona> lp =
                _ctx.Personas.
                Include(x => x.TipoPersona)
                .Include(x => x.usuarios)
               .ToList();
            return View(lp);
        }

        //metodo de crear, redireccionamiento
        public ActionResult Create() {
            List<TipoPersona> personas = _ctx.TipoPersonas.ToList();

            PersonaViewModel vm = new PersonaViewModel();
            vm.TipoPersonas = personas;


            return View("FormPersona", vm);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Crear(PersonaViewModel pvm)
        {
            string rootPath = Server.MapPath(Constantes.RUTA_GUARDAR_FOTOS_PERFIL_PERSONAS);
            Persona p = null; 
            String tipo = "";
            string filePath = "";
            string relativePath = "";
            try {
                //Validamos que todo sea correcto
                if (!ModelState.IsValid) {
                    pvm.TipoPersonas = _ctx.TipoPersonas.ToList();
                    return View("FormPersona", pvm);
                }
                HttpPostedFileBase foto = pvm.foto_archivo;
                string nombre_archivo = "";
                if (foto != null) {
                    nombre_archivo = Guid.NewGuid() + Path.GetExtension(foto.FileName);

                    if (!Directory.Exists(rootPath))
                    {
                        Directory.CreateDirectory(rootPath);
                    }
                    filePath = Path.Combine(rootPath,nombre_archivo);
                    relativePath = Constantes.RUTA_GUARDAR_FOTOS_PERFIL_PERSONAS+nombre_archivo;
                    foto.SaveAs(filePath);

                }

                if (pvm.Id == 0){
                    p = new Persona();
                    p.nombre = pvm.nombre;
                    p.apellido = pvm.apellido;
                    p.email = pvm.email;
                    p.password = pvm.password;
                    if (!String.IsNullOrEmpty(relativePath))
                    {
                        p.foto = relativePath;
                    }
                    p.TipoPersonaId = pvm.TipoPersonaId.Value;
                    
                    _ctx.Personas.Add(p);
                }
                else
                {
                    var valor = _ctx.Personas.Include(x => x.TipoPersona).Include(x => x.usuarios).SingleOrDefault(
                        m => m.Id == pvm.Id
                        );
                    if (valor != null)
                    {
                        p = valor;
                        tipo = p.TipoPersona.descripcion;
                        valor.nombre = pvm.nombre;
                        valor.apellido = pvm.apellido;
                        valor.email = pvm.email;
                        valor.password = pvm.password;
                        valor.TipoPersonaId = pvm.TipoPersonaId.Value;
                        //revisamos si tiene foto
                        if (!String.IsNullOrEmpty(valor.foto)) {
                            string ruta_absoluta = Server.MapPath(valor.foto);
                            System.IO.File.Delete(ruta_absoluta);

                        }
                        if (!String.IsNullOrEmpty(relativePath)) {
                            valor.foto = relativePath;
                        }

                    }

                }


            } catch(Exception e) {
                ModelState.AddModelError("", e);
                pvm.TipoPersonas = _ctx.TipoPersonas.ToList();
                return View("FormPersona", pvm);
            }
            //Aqui siempre se llega, de modo que se guarda
            _ctx.SaveChanges();
            
            if (pvm.Id == 0)
            {

                ApplicationUser user = new ApplicationUser { UserName = pvm.email, Email = pvm.email, PersonaId = p.Id };
                var result = UserManager.Create(user, pvm.password);
                if (result.Succeeded)
                {
                    var tipoPersona = _ctx.TipoPersonas.Where(x => x.Id == pvm.TipoPersonaId).SingleOrDefault();
                    if (null != tipoPersona)
                    {
                        UserManager.AddToRole(user.Id, tipoPersona.descripcion);
                    }
                }
                AddErrors(result);
                if (!ModelState.IsValid)
                {
                    _ctx.Personas.Remove(p);
                    _ctx.SaveChanges();
                    pvm.TipoPersonas = _ctx.TipoPersonas.ToList();
                    return View("FormPersona", pvm);
                }
            }
            else {
                //Es existente, le vamos a asignar otro rol

                ApplicationUser user = p.usuarios.FirstOrDefault();
                var tipoPersona = _ctx.TipoPersonas.Where(x => x.Id == pvm.TipoPersonaId).SingleOrDefault();
                if (null != tipoPersona)
                {
                    foreach (IdentityUserRole i in user.Roles) {
                        UserManager.RemoveFromRole(user.Id, tipo);
                    }
                    UserManager.AddToRole(user.Id, tipoPersona.descripcion);
                }

            }
            return RedirectToAction("Index");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        /*
        //Eliminar
        public ActionResult Eliminar(int id)
        {
            var tipo = _ctx.TipoPersonas.Find(id);
            _ctx.TipoPersonas.Remove(tipo);
            _ctx.SaveChanges();
            return RedirectToAction("Index");

        }*/

        public ActionResult Editar(int id) {
            var persona = _ctx.Personas.Find(id);
            List <TipoPersona> ltp = _ctx.TipoPersonas.ToList();
            PersonaViewModel vm = new PersonaViewModel(persona);
            vm.TipoPersonas = ltp;

            return View("FormPersona", vm);
        }

        public ActionResult Eliminar(int id) {
            var user = _ctx.Users.Where(x => x.PersonaId == id).FirstOrDefault();
            var persona = _ctx.Personas.Find(id);

            _ctx.Users.Remove(user);
            _ctx.SaveChanges();

            _ctx.Personas.Remove(persona);
            _ctx.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}