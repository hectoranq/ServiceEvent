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
    [Route("api/rate")]
    public class TarifasController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public TarifasController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/Tarifas
        [HttpGet]
        public IEnumerable<Tarifa> GetTarifa()
        {
            return _context.Tarifa;
        }

        // GET: api/Tarifas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarifa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tarifa = await _context.Tarifa.FindAsync(id);

            if (tarifa == null)
            {
                return NotFound();
            }

            return Ok(tarifa);
        }

        // PUT: api/Tarifas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarifa([FromRoute] int id, [FromBody] Tarifa tarifa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tarifa.IdTarifa)
            {
                return BadRequest();
            }

            _context.Entry(tarifa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarifaExists(id))
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

        // POST: api/Tarifas
        [HttpPost]
        public async Task<IActionResult> PostTarifa([FromBody] Tarifa tarifa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tarifa.Add(tarifa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarifa", new { id = tarifa.IdTarifa }, tarifa);
        }

        // DELETE: api/Tarifas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarifa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tarifa = await _context.Tarifa.FindAsync(id);
            if (tarifa == null)
            {
                return NotFound();
            }

            _context.Tarifa.Remove(tarifa);
            await _context.SaveChangesAsync();

            return Ok(tarifa);
        }

        private bool TarifaExists(int id)
        {
            return _context.Tarifa.Any(e => e.IdTarifa == id);
        }
    }
}