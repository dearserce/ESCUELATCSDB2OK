using EscuelaTCSDB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EscuelaTCSDB.Controllers.api
{
    public class PersonasController : ApiController
    {
        public ApplicationDbContext _ctx;

        public PersonasController()
        {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
        [HttpGet]
        public IHttpActionResult Get(){
            List<Persona> personasList = _ctx.Personas.ToList();
            if (personasList == null) {
                return NotFound();
            }
            return Ok(personasList);
        }
        [HttpPost]
        public Task<HttpResponseMessage> Post()
        {
            List<string> savedFilePath = new List<string>();
            // Check if the request contains multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            //Get the path of folder where we want to upload all files.
            string rootPath = HttpContext.Current.Server.MapPath(Constantes.RUTA_GUARDAR_FOTOS_PERFIL_PERSONAS);
            var provider = new MultipartFileStreamProvider(rootPath);

            //create path if not exists
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            // Read the form data.
            //If any error(Cancelled or any fault) occurred during file read , return internal server error
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }

                    string filePath = String.Empty;
                    string partialPath = String.Empty;

                    foreach (MultipartFileData dataitem in provider.FileData)
                    {
                        try
                        {
                            //Replace / from file name
                            string name = dataitem.Headers.ContentDisposition.FileName.Replace("\"", "");
                            //Create New file name using GUID to prevent duplicate file name
                            string newFileName = Guid.NewGuid() + Path.GetExtension(name);

                            //Set filepath
                            filePath = Path.Combine(rootPath, newFileName);
                            partialPath = Path.Combine(Constantes.RUTA_GUARDAR_FOTOS_PERFIL_PERSONAS, newFileName);

                            //Move file from current location to target folder.
                            File.Move(dataitem.LocalFileName, filePath);
                            savedFilePath.Add(partialPath);
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.Created, savedFilePath);
                });
            return task;
        }
    }
}
