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
    [Route("api/triviaop")]
    public class TriviaOpcionsController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public TriviaOpcionsController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/TriviaOpcions
        [HttpGet]
        public IEnumerable<TriviaOpcion> GetTriviaOpcion()
        {
            return _context.TriviaOpcion;
        }

        // GET: api/TriviaOpcions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTriviaOpcion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var triviaOpcion = await _context.TriviaOpcion.FindAsync(id);

            if (triviaOpcion == null)
            {
                return NotFound();
            }

            return Ok(triviaOpcion);
        }

        // PUT: api/TriviaOpcions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTriviaOpcion([FromRoute] int id, [FromBody] TriviaOpcion triviaOpcion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != triviaOpcion.Id)
            {
                return BadRequest();
            }

            _context.Entry(triviaOpcion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TriviaOpcionExists(id))
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

        // POST: api/TriviaOpcions
        [HttpPost]
        public async Task<IActionResult> PostTriviaOpcion([FromBody] DTOs.TriviaOpcionDTO  model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TriviaOpcion triviaOpcion = new TriviaOpcion {
                EsCorrecto = model.EsCorrecto,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                IdPregunta = model.IdPregunta,
                Titulo = model.Titulo
             };

            _context.TriviaOpcion.Add(triviaOpcion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTriviaOpcion", new { id = triviaOpcion.Id }, triviaOpcion);
        }

        // DELETE: api/TriviaOpcions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTriviaOpcion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var triviaOpcion = await _context.TriviaOpcion.FindAsync(id);
            if (triviaOpcion == null)
            {
                return NotFound();
            }

            _context.TriviaOpcion.Remove(triviaOpcion);
            await _context.SaveChangesAsync();

            return Ok(triviaOpcion);
        }

        private bool TriviaOpcionExists(int id)
        {
            return _context.TriviaOpcion.Any(e => e.Id == id);
        }
    }
}