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
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaEventoController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public AsignaEventoController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/AsignaEvento
        [HttpGet]
        public IEnumerable<AsignaEvento> GetAsignaEvento()
        {
            return _context.AsignaEvento;
        }

        // GET: api/AsignaEvento/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsignaEvento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var asignaEvento = await _context.AsignaEvento.FindAsync(id);

            if (asignaEvento == null)
            {
                return NotFound();
            }

            return Ok(asignaEvento);
        }
        // PUT: api/AsignaEvento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignaEvento([FromRoute] int id, [FromBody] AsignaEvento asignaEvento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asignaEvento.Id)
            {
                return BadRequest();
            }



            _context.Entry(asignaEvento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignaEventoExists(id))
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

        // POST: api/AsignaEvento
        [HttpPost]
        public async Task<IActionResult> PostAsignaEvento([FromBody] EntityVO.AsignaEventoVO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AsignaEvento asignaEvento = new AsignaEvento {
                FechaRelacion = DateTime.Now,
                IdEvento = model.IdEvento,
                IdUsuario = model.IdUsuario,
                IdUsuarioComp =model.IdUsuarioComp
            };


            _context.AsignaEvento.Add(asignaEvento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignaEvento", new { id = asignaEvento.Id }, asignaEvento);
        }

        // DELETE: api/AsignaEvento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignaEvento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var asignaEvento = await _context.AsignaEvento.FindAsync(id);
            if (asignaEvento == null)
            {
                return NotFound();
            }

            _context.AsignaEvento.Remove(asignaEvento);
            await _context.SaveChangesAsync();

            return Ok(asignaEvento);
        }

        private bool AsignaEventoExists(int id)
        {
            return _context.AsignaEvento.Any(e => e.Id == id);
        }
    }
}