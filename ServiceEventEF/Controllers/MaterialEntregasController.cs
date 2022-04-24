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
    [Route("api/material_inscrip")]
    public class MaterialEntregasController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public MaterialEntregasController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/MaterialEntregas
        [HttpGet]
        public IEnumerable<MaterialEntrega> GetMaterialEntrega()
        {
            return _context.MaterialEntrega;
        }

        // GET: api/MaterialEntregas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterialEntrega([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var materialEntrega = await _context.MaterialEntrega.FindAsync(id);

            if (materialEntrega == null)
            {
                return NotFound();
            }

            return Ok(materialEntrega);
        }

        // PUT: api/MaterialEntregas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterialEntrega([FromRoute] int id, [FromBody] MaterialEntrega materialEntrega)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materialEntrega.Id)
            {
                return BadRequest();
            }

            _context.Entry(materialEntrega).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialEntregaExists(id))
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

        // POST: api/MaterialEntregas
        [HttpPost]
        public async Task<IActionResult> PostMaterialEntrega([FromBody] MaterialEntrega materialEntrega)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MaterialEntrega.Add(materialEntrega);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterialEntrega", new { id = materialEntrega.Id }, materialEntrega);
        }

        // DELETE: api/MaterialEntregas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterialEntrega([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var materialEntrega = await _context.MaterialEntrega.FindAsync(id);
            if (materialEntrega == null)
            {
                return NotFound();
            }

            _context.MaterialEntrega.Remove(materialEntrega);
            await _context.SaveChangesAsync();

            return Ok(materialEntrega);
        }

        private bool MaterialEntregaExists(int id)
        {
            return _context.MaterialEntrega.Any(e => e.Id == id);
        }
    }
}