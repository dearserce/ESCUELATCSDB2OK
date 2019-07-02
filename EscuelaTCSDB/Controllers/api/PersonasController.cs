using EscuelaTCSDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
