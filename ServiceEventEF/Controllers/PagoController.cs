using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceEventEF.Models;

namespace ServiceEventEF.Controllers
{
    [Produces("application/json")]
    [Route("api/payment")]
    public class PagoController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public PagoController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/Pago
        [HttpGet]
        public IEnumerable<Pago> GetPago()
        {
            return _context.Pago;
        }

        // GET: api/Pago/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPago([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pago = await _context.Pago.FindAsync(id);

            if (pago == null)
            {
                return NotFound();
            }

            return Ok(pago);
        }

        // PUT: api/Pago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago([FromRoute] int id, [FromBody] Pago pago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pago.IdPago)
            {
                return BadRequest();
            }

            _context.Entry(pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pago
        [HttpPost]
        public async Task<IActionResult> PostPago([FromBody] DTOs.PagoDTO model)
        {
            if (this.PagoUserEvent(model.Id_Contacto, model.Id_Evento))
            {
                return NotFound(new { success = "Contacto ya realizo el pago" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Pago pago = new Pago {
                Confirmacion = model.Confirmacion,
                Descripcion = model.Descripcion,
                Estado = model.Estado,
                EstadoNavigation = null,
                FechaConformacion = DateTime.Now,
                FechaSolicitud = DateTime.Now,
                IdFormaPago = model.IdFormaPago,
                IdPago = model.IdPago,
                Inscripcion = null,
                Moneda = model.Moneda,
                MontoMonetario = model.MontoMonetario,
                Solicitud = model.Solicitud,
                IdEvento = model.Id_Evento,
                IdContacto = model.Id_Contacto
            };


            _context.Pago.Add(pago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPago", new { id = pago.IdPago }, pago);
        }

        // DELETE: api/Pago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pago = await _context.Pago.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.Pago.Remove(pago);
            await _context.SaveChangesAsync();

            return Ok(pago);
        }

        private bool PagoExists(int id)
        {
            return _context.Pago.Any(e => e.IdPago == id);
        }

        private bool PagoUserEvent(int idContacto, int idEvento) {
            return _context.Pago.Any(e => e.IdContacto == idContacto && e.IdEvento == idEvento);
        }
    }
}