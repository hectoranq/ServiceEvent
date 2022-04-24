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
    [Route("api/triviapre")]
    public class TriviaPreguntasController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public TriviaPreguntasController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/triviapre
        [HttpGet]
        public IEnumerable<TriviaPregunta> GetTriviaPregunta()
        {
            return _context.TriviaPregunta;
        }

        // GET: api/triviapre/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTriviaPregunta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var triviaPregunta = await _context.TriviaPregunta.FindAsync(id);

            if (triviaPregunta == null)
            {
                return NotFound();
            }

            return Ok(triviaPregunta);
        }
        // trivia por cuestionario
        [HttpGet("question/{id}")]
        public async Task<IActionResult> GetTriviaPreguntaCuestionario([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var triviaPregunta = await _context.TriviaPregunta.Where(x => x.IdCuestionario == id).ToListAsync();

            if (triviaPregunta == null)
            {
                return NotFound();
            }

            return Ok(triviaPregunta);
        }

        // PUT: api/TriviaPreguntas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTriviaPregunta([FromRoute] int id, [FromBody] TriviaPregunta triviaPregunta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != triviaPregunta.Id)
            {
                return BadRequest();
            }

            _context.Entry(triviaPregunta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TriviaPreguntaExists(id))
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

        // POST: api/TriviaPreguntas
        [HttpPost]
        public async Task<IActionResult> PostTriviaPregunta([FromBody] DTOs.TriviaPreguntaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int numP = _context.TriviaPregunta.Count();
            TriviaPregunta triviaPregunta = new TriviaPregunta { 
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                IdCuestionario = model.IdCuestionario,
                Indicio = model.Indicio,
                RutaImagen = model.RutaImagen,
                Titulo = model.Titulo,
                NumPregunta = numP + 1

            };

            _context.TriviaPregunta.Add(triviaPregunta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTriviaPregunta", new { id = triviaPregunta.Id }, triviaPregunta);
        }

        // DELETE: api/TriviaPreguntas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTriviaPregunta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var triviaPregunta = await _context.TriviaPregunta.FindAsync(id);
            if (triviaPregunta == null)
            {
                return NotFound();
            }

            _context.TriviaPregunta.Remove(triviaPregunta);
            await _context.SaveChangesAsync();

            return Ok(triviaPregunta);
        }

        private bool TriviaPreguntaExists(int id)
        {
            return _context.TriviaPregunta.Any(e => e.Id == id);
        }
    }
}