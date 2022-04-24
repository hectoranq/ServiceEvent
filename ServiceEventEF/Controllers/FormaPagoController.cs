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
    [Route("api/mth_payment")]
    public class FormaPagoController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public FormaPagoController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/FormaPago
        [HttpGet]
        public IEnumerable<FormaPago> GetFormaPago()
        {
            return _context.FormaPago;
        }

        // GET: api/FormaPago/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormaPago([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formaPago = await _context.FormaPago.FindAsync(id);

            if (formaPago == null)
            {
                return NotFound();
            }

            return Ok(formaPago);
        }

        // PUT: api/FormaPago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormaPago([FromRoute] int id, [FromBody] FormaPago formaPago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != formaPago.IdFormaPago)
            {
                return BadRequest();
            }

            _context.Entry(formaPago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormaPagoExists(id))
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

        // POST: api/FormaPago
        [HttpPost]
        public async Task<IActionResult> PostFormaPago([FromBody] FormaPago formaPago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FormaPago.Add(formaPago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormaPago", new { id = formaPago.IdFormaPago }, formaPago);
        }

        // DELETE: api/FormaPago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormaPago([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formaPago = await _context.FormaPago.FindAsync(id);
            if (formaPago == null)
            {
                return NotFound();
            }

            _context.FormaPago.Remove(formaPago);
            await _context.SaveChangesAsync();

            return Ok(formaPago);
        }

        private bool FormaPagoExists(int id)
        {
            return _context.FormaPago.Any(e => e.IdFormaPago == id);
        }
    }
}