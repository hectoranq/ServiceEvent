using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceEventEF.Models;
using ServiceEventEF.EntityVO;

namespace ServiceEventEF.Controllers
{
    [Produces("application/json")]
    [Route("api/parameters")]
    public class ParametrosController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public ParametrosController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        [HttpGet("paises")]
        public IActionResult GetPaises()
        {

            var result = _context.Parametros.Where(s => s.IdTipo == 3);
            //  est_repo.setting = config;
            if (result == null)
            {
                return NotFound();
            }

            List<SelectDatos> prs_paises = new List<SelectDatos>();
            foreach (var item in result)
            {
                SelectDatos select = new SelectDatos {
                    Id = item.NombreTipo,
                    Descripcion = item.NombreParametro
                 };
                prs_paises.Add(select);
            }

            return Ok(prs_paises);
            //return new JsonResult(result);
        }

        // GET: api/Parametros
        [HttpGet]
        public IEnumerable<Parametros> GetParametros()
        {
            return _context.Parametros;
        }

        // GET: api/Parametros/5
        [HttpGet("{id}")]
        public IActionResult GetParametros([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parametros = _context.Parametros.Where(x => x.IdTipo == id);
            if (parametros == null)
            {
                return NotFound();
            }

         /*   List<CategoriaVO> vOs = new List<CategoriaVO>();
            foreach (var item in parametros)
            {
                CategoriaVO categoriaVO = new CategoriaVO {
                    Codigo = item.NombreTipo,
                    Descripcion = item.Descripcion,
                    Nombre = item.NombreParametro
                };
                vOs.Add(categoriaVO);
            }*/

            return Ok(parametros);
        }

        // PUT: api/Parametros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParametros([FromRoute] int id, [FromBody] Parametros parametros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parametros.IdParametro)
            {
                return BadRequest();
            }

            _context.Entry(parametros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametrosExists(id))
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

        // POST: api/Parametros
        [HttpPost("create")]
        public async Task<IActionResult> PostParametros([FromBody] Parametros parametros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (parametros.IdParametro != 0)
            {
                _context.Entry(parametros).State = EntityState.Modified;
            }
            else
            {
                _context.Parametros.Add(parametros);
            }


           
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParametros", new { id = parametros.IdParametro }, parametros);
        }

        // DELETE: api/Parametros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParametros([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parametros = await _context.Parametros.FindAsync(id);
            if (parametros == null)
            {
                return NotFound();
            }

            _context.Parametros.Remove(parametros);
            await _context.SaveChangesAsync();

            return Ok(parametros);
        }

        // BUSQUEDA DE LIKE: api/parameters/codigo/ACT
        [HttpGet("codigo/{id}")]
        public async Task<IActionResult> ParametrosCodigo([FromRoute] string  id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parametros = await _context.Parametros.FromSql("select * from Parametros where Nombre_Tipo like '" + id + "%'").ToListAsync();
            
            if (parametros == null)
            {
                return NotFound();
            }

            List<EntityVO.ParametroVO> vosParametros = new List<ParametroVO>();
            foreach (var item in parametros)
            {
                EntityVO.ParametroVO parametroVO = new ParametroVO {
                    descripcion = item.NombreParametro,
                    id = item.IdParametro
                };
                vosParametros.Add(parametroVO);
            }

            return Ok(vosParametros);
        }



        private bool ParametrosExists(int id)
        {
            return _context.Parametros.Any(e => e.IdParametro == id);
        }
    }
}