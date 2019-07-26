using EscuelaTCSDB.Models;
using EscuelaTCSDB.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;
using System.IO;
using System.Web;

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
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]
        public ActionResult Excel()
        {
            byte[] fileData = null;
            GrupoPersonaExcelViewModel gpevm = new GrupoPersonaExcelViewModel();
            string nombre = DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + Constantes.XLSX;
            string contentType = MimeMapping.GetMimeMapping(nombre);
            Response.AppendHeader(Constantes.CONTENT_DISPOSITION_HEADER_NAME, String.Format(Constantes.CONTEN_DISPOSITION_HEADER_VALUE, nombre));

            using (var e = new ExcelPackage())
            using (var memoryStream = new MemoryStream())
            {
                e.Workbook.Worksheets.Add("Tabla");
                e.Workbook.Worksheets.Add("Personas");
                e.Workbook.Worksheets.Add("Materias");
                e.Workbook.Worksheets.Add("Grupo");
                e.Workbook.Worksheets.Add("Ciclo");
                e.Workbook.Worksheets.Add("Profesor");

                var wsTabla = e.Workbook.Worksheets[1];
                var wsPersona = e.Workbook.Worksheets[2];
                var wsMateria = e.Workbook.Worksheets[3];
                var wsGrupo = e.Workbook.Worksheets[4];
                var wsCiclo = e.Workbook.Worksheets[5];
                var wsProfesor = e.Workbook.Worksheets[6];


                //Headers de Tabla
                List<string> col_headers = new List<string>();
                col_headers.Add("Persona");
                col_headers.Add("Materia");
                col_headers.Add("Grupo");
                col_headers.Add("Ciclo");
                col_headers.Add("Profesor");

                //Acomodo de Headers
                for (int i = 0; i < col_headers.Count; i++) {
                    wsTabla.Cells[1, i+1].Value = 
                        col_headers[i];
                }

                //listas que van a iterar los worksheets
                List <Materia> materias = _ctx.Materias.Where(x => x.activo == true).ToList();
                List<Grupo> grupos = _ctx.Grupos.ToList();
                List<Ciclo> ciclos = _ctx.Ciclos.ToList();
                List<Persona> personas = _ctx.Personas.Where(x => x.TipoPersonaId == TipoPersona.Alumno).ToList();
                List<Persona> profesores = _ctx.Personas.Where(x => x.TipoPersonaId == TipoPersona.Profesor).ToList();

                //ahora definimos las listas con valores aceptados
                var dvListMateria = wsTabla.DataValidations.AddListValidation("B$2:B$50");
                var dvListPersona = wsTabla.DataValidations.AddListValidation("A$2:A$50");
                var dvListGrupo = wsTabla.DataValidations.AddListValidation("C$2:C$50");
                var dvListCiclo = wsTabla.DataValidations.AddListValidation("D$2:D$50");
                var dvListProfe = wsTabla.DataValidations.AddListValidation("E$2:E$50");

                //llenamos el worksheet y listas

                //materias
                for (int i = 0; i < materias.Count; i++) {
                    string valorContenido = materias[i].Id+ "." + materias[i].nombre;
                    wsMateria.Cells[i + 1, 1].Value = valorContenido;
                    dvListMateria.Formula.Values.Add(valorContenido);
                }
                //Personas
                for (int i = 0; i < personas.Count; i++)
                {
                    string valorContenido = personas[i].Id + "." + personas[i].nombre+" "+personas[i].apellido;
                    wsPersona.Cells[i + 1, 1].Value = valorContenido;
                    dvListPersona.Formula.Values.Add(valorContenido);
                }
                //grupo
                for (int i = 0; i < grupos.Count; i++)
                {
                    string valorContenido = grupos[i].Id + "." + grupos[i].codigo;
                    wsGrupo.Cells[i + 1, 1].Value = valorContenido;
                    dvListGrupo.Formula.Values.Add(valorContenido);
                }
                //ciclo
                for (int i = 0; i < ciclos.Count; i++)
                {
                    string valorContenido = ciclos[i].Id + "." + ciclos[i].fecha_inicio.Value.ToString("dd/MM/yyyy") + "_" + ciclos[i].fecha_fin.Value.ToString("dd/MM/yyyy");
                    wsCiclo.Cells[i + 1, 1].Value = valorContenido;
                    dvListCiclo.Formula.Values.Add(valorContenido);
                }
                //Profesor
                for (int i = 0; i < profesores.Count; i++)
                {
                    string valorContenido = profesores[i].Id + "." + profesores[i].nombre + " "+profesores[i].apellido;
                    wsProfesor.Cells[i + 1, 1].Value = valorContenido;
                    dvListProfe.Formula.Values.Add(valorContenido);
                }

                e.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                fileData = memoryStream.ToArray();

            }
            return File(fileData, contentType);
        }
        public ActionResult Carga() {
            GrupoPersonaExcelViewModel g = new GrupoPersonaExcelViewModel();
            return View("FormExcel",g);
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

                            if (exw.Cells[row, 1].Value != null &&
                                exw.Cells[row, 2].Value != null &&
                                exw.Cells[row, 3].Value != null &&
                                exw.Cells[row, 4].Value != null &&
                                exw.Cells[row, 5].Value != null)
                            {
                                GrupoPersona gp = new GrupoPersona();
                                char delimitador = '.';
                                string[] PersonaId = exw.Cells[row, 1].Text.Split(delimitador);
                                string[] GrupoId = exw.Cells[row, 3].Text.Split(delimitador);
                                string[] MateriaId = exw.Cells[row, 2].Text.Split(delimitador);
                                string[] CicloId = exw.Cells[row, 4].Text.Split(delimitador);
                                string[] ProfesorId = exw.Cells[row, 5].Text.Split(delimitador);
                                 
                                gp.PersonaId = int.Parse(PersonaId[0]); 
                                gp.GrupoId = int.Parse(GrupoId[0]); 
                                gp.MateriaId = int.Parse(MateriaId[0]); 
                                gp.CicloId = int.Parse(CicloId[0]); 
                                gp.ProfesorId = int.Parse(ProfesorId[0]);

                                lgp.Add(gp);
                            }
                            int x = 4;
                        }
                        //ahora guardamos 
                        for (int i = 0; i < lgp.Count; i++) {
                            _ctx.GrupoPersonas.Add(lgp[i]);
                        }
                        _ctx.SaveChanges();
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