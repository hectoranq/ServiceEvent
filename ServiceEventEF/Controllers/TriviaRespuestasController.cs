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
    [Route("api/triviaresp")]
    public class TriviaRespuestasController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public TriviaRespuestasController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/TriviaRespuestas
        [HttpGet]
        public IEnumerable<TriviaRespuesta> GetTriviaRespuesta()
        {
            return _context.TriviaRespuesta;
        }

        // GET: api/TriviaRespuestas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTriviaRespuesta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var triviaRespuesta = await _context.TriviaRespuesta.FindAsync(id);

            if (triviaRespuesta == null)
            {
                return NotFound();
            }

            return Ok(triviaRespuesta);
        }

        // PUT: api/TriviaRespuestas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTriviaRespuesta([FromRoute] int id, [FromBody] TriviaRespuesta triviaRespuesta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != triviaRespuesta.Id)
            {
                return BadRequest();
            }

            _context.Entry(triviaRespuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TriviaRespuestaExists(id))
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

        // POST: api/TriviaRespuestas
        [HttpPost]
        public async Task<IActionResult> PostTriviaRespuesta([FromBody] TriviaRespuesta triviaRespuesta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TriviaRespuesta.Add(triviaRespuesta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTriviaRespuesta", new { id = triviaRespuesta.Id }, triviaRespuesta);
        }

        // DELETE: api/TriviaRespuestas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTriviaRespuesta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var triviaRespuesta = await _context.TriviaRespuesta.FindAsync(id);
            if (triviaRespuesta == null)
            {
                return NotFound();
            }

            _context.TriviaRespuesta.Remove(triviaRespuesta);
            await _context.SaveChangesAsync();

            return Ok(triviaRespuesta);
        }

        private bool TriviaRespuestaExists(int id)
        {
            return _context.TriviaRespuesta.Any(e => e.Id == id);
        }
    }
}