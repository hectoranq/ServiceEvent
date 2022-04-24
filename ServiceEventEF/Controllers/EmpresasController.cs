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
    [Route("api/company")]
    public class EmpresasController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public EmpresasController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/Empresas
        [HttpGet]
        public IEnumerable<Empresa> GetEmpresa()
        {
            return _context.Empresa;
        }

        // GET: api/Empresas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empresa = await _context.Empresa.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        // PUT: api/Empresas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa([FromRoute] int id, [FromBody] Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empresa.IdEmpresa)
            {
                return BadRequest();
            }

            _context.Entry(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
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

        // POST: api/Empresas
        [HttpPost]
        public async Task<IActionResult> PostEmpresa([FromBody] DTOs.EmpresaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Empresa empresa = new Empresa {
                CorreoElectronico = model.CorreoElectronico,
                CupoMaximoAsistentes = model.CupoMaximoAsistentes,
                Direccion = model.Direccion,
                Estado = 1,
                Nit = model.Nit,
                Nombre = model.Nombre,
                Pais = model.Pais,
                TelefonoFijo = model.TelefonoFijo,
                TelefonoMovil = model.TelefonoMovil,
                TipoIdentificacion = model.TipoIdentificacion
            };

            _context.Empresa.Add(empresa);
            await _context.SaveChangesAsync();

            return Ok( new { id = empresa.IdEmpresa});
        }

        // DELETE: api/Empresas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var empresa = await _context.Empresa.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            _context.Empresa.Remove(empresa);
            await _context.SaveChangesAsync();

            return Ok(empresa);
        }

        private bool EmpresaExists(int id)
        {
            return _context.Empresa.Any(e => e.IdEmpresa == id);
        }


        [HttpGet("lista")]
        public IActionResult ListaEmpresa()
        {
            try
            {
                var events = _context.Empresa;
                List<EntityVO.ParametroVO> param = new List<EntityVO.ParametroVO>();
                foreach (var item in events)
                {
                    EntityVO.ParametroVO parametroVO = new EntityVO.ParametroVO
                    {
                        descripcion = item.Nombre,
                        id = item.IdEmpresa
                    };
                    param.Add(parametroVO);
                }

                return Ok(param);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
    }
}