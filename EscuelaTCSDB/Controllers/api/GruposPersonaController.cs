using EscuelaTCSDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;


namespace EscuelaTCSDB.Controllers.api
{
    public class GruposPersonaController : ApiController
    {
        public ApplicationDbContext _ctx;

        public GruposPersonaController()
        {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }

        [HttpGet]
        public IHttpActionResult ListaAsignacion() {
            List<GrupoPersona> gpl = _ctx.GrupoPersonas.
              Include(x => x.Grupo).
                Include(x => x.Materia).
                Include(x => x.Persona).
                Include(x => x.Ciclo).
                Include(x => x.Profesor).OrderBy(x => x.Persona.nombre).ToList();

            return Ok(gpl);
        }
    }
}
