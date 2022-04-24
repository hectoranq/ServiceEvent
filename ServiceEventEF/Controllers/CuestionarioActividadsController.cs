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
    [Route("api/question")]
    public class CuestionarioActividadsController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public CuestionarioActividadsController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/CuestionarioActividads
        [HttpGet]
        public IEnumerable<CuestionarioActividad> GetCuestionarioActividad()
        {
            return _context.CuestionarioActividad;
        }

        // GET: api/question/5
        [HttpGet("{id}")]
        public IActionResult GetCuestionarioActividad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cuestionarioActividad =  _context.CuestionarioActividad.Where(x => x.IdActividad == id).ToList();
            List<DTOs.CuestionarioReportDTO> cuestionarios = new List<DTOs.CuestionarioReportDTO>();
            foreach (var item in cuestionarioActividad)
            {
                var preguntas = _context.TriviaPregunta.Where(x => x.IdCuestionario == item.Id).Count();
                if (preguntas > 0)
                {
                    DTOs.CuestionarioReportDTO cuestionarioReport = new DTOs.CuestionarioReportDTO
                    {
                        Id = item.Id,
                        Completados = 0,
                        ContactosInscritos = 0,
                        TipoEncuesta = "",
                        Titulo = item.Titulo
                    };
                    cuestionarios.Add(cuestionarioReport);
                }

            }
            if (cuestionarios == null)
            {
                return NotFound();
            }

            return Ok(cuestionarios);
        }

      

        // GET: api/question/quiz/5
        [HttpGet("quiz/{id}")]
        public IActionResult GetQuiz([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cuestionarioActividad =  _context.CuestionarioActividad.Where(x => x.Id == id).FirstOrDefault();

            var pregunta = _context.TriviaPregunta.Where(x => x.IdCuestionario == cuestionarioActividad.Id ).ToList();
            List<DTOs.QuizDTO> quizs = new List<DTOs.QuizDTO>();
            foreach (var item in pregunta)
            {
                DTOs.QuizDTO quiz = new DTOs.QuizDTO();
                quiz.Titulo = item.Titulo;
                var opciones = _context.TriviaOpcion.Where(x => x.IdPregunta == item.Id).ToList();
                List<DTOs.TriviaOpcionDTO> opcionesDTO = new List<DTOs.TriviaOpcionDTO>();
                foreach (var opcion in opciones)
                {
                    DTOs.TriviaOpcionDTO triviaOpcion = new DTOs.TriviaOpcionDTO { 
                        Titulo = opcion.Titulo,
                        EsCorrecto = bool.Parse( opcion.EsCorrecto.ToString())
                    };
                    opcionesDTO.Add(triviaOpcion);
                }
                quiz.Opciones = opcionesDTO;

                quizs.Add(quiz);
            }


            if (quizs == null)
            {
                return NotFound();
            }

            return Ok( quizs );
        }


        // PUT: api/CuestionarioActividads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuestionarioActividad([FromRoute] int id, [FromBody] CuestionarioActividad cuestionarioActividad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuestionarioActividad.Id)
            {
                return BadRequest();
            }

            _context.Entry(cuestionarioActividad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuestionarioActividadExists(id))
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

        // POST: api/CuestionarioActividads
        [HttpPost]
        public async Task<IActionResult> PostCuestionarioActividad([FromBody] DTOs.CuestionarioActividadDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CuestionarioActividad cuestionario = new CuestionarioActividad {
                IdActividad = model.IdActividad,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                IdInscritos = model.IdInscritos,
                Titulo = model.Titulo
             };

            _context.CuestionarioActividad.Add(cuestionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuestionarioActividad", new { id = cuestionario.Id }, cuestionario);
        }

        // DELETE: api/CuestionarioActividads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuestionarioActividad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cuestionarioActividad = await _context.CuestionarioActividad.FindAsync(id);
            if (cuestionarioActividad == null)
            {
                return NotFound();
            }

            _context.CuestionarioActividad.Remove(cuestionarioActividad);
            await _context.SaveChangesAsync();

            return Ok(cuestionarioActividad);
        }

        private bool CuestionarioActividadExists(int id)
        {
            return _context.CuestionarioActividad.Any(e => e.Id == id);
        }
    }
}