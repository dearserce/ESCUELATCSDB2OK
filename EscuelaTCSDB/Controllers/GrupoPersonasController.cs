using EscuelaTCSDB.Models;
using EscuelaTCSDB.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;
using System.IO;

namespace EscuelaTCSDB.Controllers
{
    [Authorize(Roles = "Directivo")]
    public class GrupoPersonasController : Controller
    {
        public ApplicationDbContext _ctx;
        public GrupoPersonasController()
        {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
        public ActionResult Index() {
            return View();
        }
         public ActionResult Create() {
            List<Grupo> grupos = _ctx.Grupos.ToList();
            List<Materia> materias = _ctx.Materias.ToList();
            List<Ciclo> ciclos = _ctx.Ciclos.ToList();
            List<Persona> personas = _ctx.Personas.Where(x => x.TipoPersonaId == TipoPersona.Alumno).ToList();
            List<Persona> profesores = _ctx.Personas.Where(x => x.TipoPersonaId == TipoPersona.Profesor).ToList();

            GrupoPersonaViewModel gpvm = new GrupoPersonaViewModel();
            gpvm.grupoList = grupos;
            gpvm.materiaList = materias;
            gpvm.cicloList = ciclos;
            gpvm.personaList = personas;
            gpvm.profesorList = profesores;

            return View("FormGrupoPersonas", gpvm); 
        }

        //Excel Vista
        public ActionResult Excel() {
            return View("FormExcel");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       public ActionResult SubirExcel(GrupoPersonaExcelViewModel gpevm) {
            if (!ModelState.IsValid) {
                return View("FormExcel", gpevm);
            }

            //En este punto tenemos el View model con informacion.
            //generamos un path.
            String path = Server.MapPath(Constantes.RUTA_TEMPORAL_EXCEL_GRUPOPERSONAS + gpevm.file.FileName);
            
            //revisamos si existe el archivo, al final de todo esto se elimina, es decir, no debe de existir el archivo.
            if (!System.IO.File.Exists(path)) {
                gpevm.file.SaveAs(path);

                FileInfo fi = new FileInfo(path);
                if (fi.Exists) {
                    using (ExcelPackage ep = new ExcelPackage(fi)) {

                        //Implícitamente solo existe 1 worksheet, de modo que tomamos el primero
                        ExcelWorksheet exw = ep.Workbook.Worksheets[1];

                        //obtenemos las columnas y filas.
                        int columCount = exw.Dimension.End.Column;
                        int rowCount = exw.Dimension.End.Row;

                        //generamos una lista vacia
                        List<GrupoPersona> lgp = new List<GrupoPersona>();

                        for (int row = 2; row < rowCount; row++) {
                            GrupoPersona gp = new GrupoPersona();
                            gp.PersonaId = int.Parse(exw.Cells[row,1].Text);
                            gp.GrupoId = int.Parse(exw.Cells[row,2].Text);
                            gp.MateriaId = int.Parse(exw.Cells[row, 3].Text);
                            gp.CicloId = int.Parse(exw.Cells[row, 4].Text);
                            gp.ProfesorId = int.Parse(exw.Cells[row, 5].Text);

                            lgp.Add(gp);
                        }
                        //ahora guardamos
                        for (int i = 0; i < lgp.Count; i++) {
                            _ctx.GrupoPersonas.Add(lgp[i]);
                        }
                        _ctx.SaveChanges();
                        Index();
                    }
                }
              }
            return View("FormExcel", gpevm);
        }
        //Crear
        public ActionResult Crear(GrupoPersonaViewModel gpvm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //Algo salio mal, de modo que volvemos a la lista, pero 
                    //se tienen que llenar los dropdown de nuevo
                    gpvm.grupoList = _ctx.Grupos.ToList();
                    gpvm.materiaList = _ctx.Materias.ToList();
                    gpvm.cicloList = _ctx.Ciclos.ToList();
                    gpvm.personaList = _ctx.Personas.Where(x => x.TipoPersonaId == TipoPersona.Alumno).ToList();
                    gpvm.profesorList = _ctx.Personas.Where(x => x.TipoPersonaId  == TipoPersona.Profesor).ToList();

                    //
                    return View("FormGrupoPersonas", gpvm);
                }
                GrupoPersona gp = null;
                if (gpvm.Id == 0)
                {
                     gp = new GrupoPersona();
                    //llenamos la informacion
                    gp.GrupoId= gpvm.GrupoId;
                    gp.MateriaId = gpvm.MateriaId;
                    gp.CicloId = gpvm.CicloId;
                    gp.PersonaId = gpvm.PersonaId;
                    gp.ProfesorId = gpvm.ProfesorId;
                    
                    _ctx.GrupoPersonas.Add(gp);                                                                            
                }
                else
                {
                    //Estan editando
                     gp = _ctx.GrupoPersonas.SingleOrDefault(x => x.Id == gpvm.Id);
                    
                    if (gp != null)
                    {
                        //es que quieren editar y realmente existe, parseamos valores
                        gp.GrupoId = gpvm.GrupoId;
                        gp.MateriaId = gpvm.MateriaId;
                        gp.CicloId = gpvm.CicloId;
                        gp.PersonaId = gpvm.PersonaId;
                        gp.ProfesorId = gpvm.ProfesorId;
                    }
                    
                }


                _ctx.SaveChanges();
                //aqui validamos que fue un registro nuevo
                if (gpvm.Id == 0) {
                    //es un registro nuevo
                    int id_gp = gp.Id;
                    // comentado por que daba nulo . 
                    //int periodos = gp.Ciclo.Modalidad.n_periodos;
                    var ciclo = _ctx.Ciclos.Include(x => x.Modalidad).SingleOrDefault(x => x.Id== gp.CicloId);
                    int periodos = ciclo.Modalidad.n_periodos;
                    for (int i = 1; i <= periodos; i++)  {
                        //realiamos un insert
                        GpPeriodo gpp = new GpPeriodo();

                        gpp.GrupoPersonaId = id_gp;
                        gpp.periodo = i;
                        _ctx.GpPeriodos.Add(gpp);

                    }
                    _ctx.SaveChanges();
                        }
            }
            catch(Exception e)
            {

                return View("FormGrupoPersona",e);
            }
            
            return RedirectToAction("Index");
        }
        //Editar
        public ActionResult Editar(int id)
        {
            var grupoPersona = _ctx.GrupoPersonas.Find(id);
            
            List<Grupo> lgrupo = _ctx.Grupos.ToList();
            List<Materia> lmateria = _ctx.Materias.ToList();
            List<Ciclo> lciclo = _ctx.Ciclos.ToList();
            List<Persona> lpersona = _ctx.Personas.Where(x => x.TipoPersonaId == TipoPersona.Alumno).ToList();
            List<Persona> lprofe = _ctx.Personas.Where(x => x.TipoPersonaId == TipoPersona.Profesor).ToList();

            GrupoPersonaViewModel gpvm = new GrupoPersonaViewModel(grupoPersona);
            gpvm.grupoList = lgrupo;
            gpvm.materiaList = lmateria;
            gpvm.cicloList = lciclo;
            gpvm.personaList = lpersona;
            gpvm.profesorList = lprofe;

            return View("FormGrupoPersonas", gpvm);
        }
        //eliminar
        public ActionResult Eliminar(int id)
        {
                  var grupoPersonas= _ctx.GrupoPersonas.Find(id);
                  _ctx.GrupoPersonas.Remove(grupoPersonas);
                  _ctx.SaveChanges();
                  return RedirectToAction("Index");
              
            
        }

    }
}