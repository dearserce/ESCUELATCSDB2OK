using EscuelaTCSDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace EscuelaTCSDB.Controllers.api
{
    public class TiposPersonaController : ApiController
    {

        public ApplicationDbContext _ctx;

        public TiposPersonaController()
        {
            _ctx = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<TipoPersona> tipoPersonasList = _ctx.TipoPersonas.OrderBy(x => x.Id).ToList();
            if (tipoPersonasList == null)
            {
                return NotFound();
            }
            return Ok(tipoPersonasList);
        }
        [HttpPost]
        public IHttpActionResult Post(TipoPersonaDTO tpDTO) {
            try
            {
                TipoPersona tpv = null;
                tpv = _ctx.TipoPersonas.Where(x => x.descripcion == tpDTO.descripcion).FirstOrDefault();
                if (tpv != null) {
                    return BadRequest("Existente");
                }
                tpv = new TipoPersona();
                tpv.descripcion = tpDTO.descripcion;
                _ctx.TipoPersonas.Add(tpv);
                _ctx.SaveChanges();
                tpDTO.Id = tpv.Id;
                return Created(new Uri(Request.RequestUri + "/" + tpDTO.Id), tpDTO);
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult Put(TipoPersonaDTO tpDTO) {
            try {
                TipoPersona tpv = null;
                tpv = _ctx.TipoPersonas.Where(x => x.Id == tpDTO.Id).FirstOrDefault();
                if (tpv == null) {
                    return BadRequest("Registro no existente");
                }
                tpv.descripcion = tpDTO.descripcion;
                _ctx.SaveChanges();
                return Ok();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        public IHttpActionResult Delete(TipoPersonaDTO tpDTO) {
            try {
                TipoPersona tpv = null;
                tpv = _ctx.TipoPersonas.Where(x => x.Id == tpDTO.Id).FirstOrDefault();
                if (tpv == null) {
                    return BadRequest("Registro no existente");
                }
                _ctx.TipoPersonas.Remove(tpv);
                _ctx.SaveChanges();
                return Ok();
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        
    }
}

