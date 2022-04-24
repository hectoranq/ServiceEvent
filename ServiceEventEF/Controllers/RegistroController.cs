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
    [Route("api/registry")]
    public class RegistroController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public RegistroController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/Registro
        [HttpGet]
        public IEnumerable<Registro> GetRegistro()
        {
            return _context.Registro;
        }

        // GET: api/Registro/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistro([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registro = await _context.Registro.FindAsync(id);

            if (registro == null)
            {
                return NotFound();
            }

            return Ok(registro);
        }

        // GET: api/Registro/5
        [HttpGet("evento/{idevento}/actividad/{idactividad}")]
        public async Task<IActionResult> GetRegistroActividadAsistencia(int idevento, int idactividad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string sQuery = "select con.Documento_Identidad as NroDocumento, con.Nombres as Nombre, con.Apellidos as Apellidos, reg.Descripcion as Descripcion, reg.Id_Evento, '' as Ciudad, '' as CostoEvento, '' as Cupo_Maximo_Inscripciones, '' as Destacado, '' as Direccion from Registro reg inner join Inscripcion ins on reg.Id_Inscripcion = ins.Id_Inscripcion inner join Contacto con on con.Id_Contacto = ins.Id_Contacto  where reg.Id_Evento = " + idevento + " and reg.Id_Actividad = " + idactividad;
                var registro = _context.Evento.FromSql(sQuery).ToList();
                if (registro == null)
                {
                    return NotFound();
                }
                return Ok(registro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
           

           
        }



        // PUT: api/Registro/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistro([FromRoute] int id, [FromBody] Registro registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registro.IdRegistro)
            {
                return BadRequest();
            }

            _context.Entry(registro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroExists(id))
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

        // POST: api/Registro
        [HttpPost]
        public async Task<IActionResult> PostRegistro([FromBody] Registro registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (this.RegistroEventoExists(registro.IdInscripcion, registro.IdEvento, registro.Descripcion))
            {
                return NotFound();
            }
            _context.Registro.Add(registro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistro", new { id = registro.IdRegistro }, registro);
        }

        // DELETE: api/Registro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistro([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registro = await _context.Registro.FindAsync(id);
            if (registro == null)
            {
                return NotFound();
            }

            _context.Registro.Remove(registro);
            await _context.SaveChangesAsync();

            return Ok(registro);
        }

        private bool RegistroExists(int id)
        {
            return _context.Registro.Any(e => e.IdRegistro == id);
        }

        private bool RegistroEventoExists(int IdInscripcion, int IdEvento, string descripcion)
        {
            return _context.Registro.Any(e => e.IdInscripcion == IdInscripcion && e.IdEvento == IdEvento && e.Descripcion.Trim() == descripcion.Trim());
        }


        [HttpPost("controlasistente")]
        public async Task<IActionResult> GetInscripcionEvento([FromBody] EntityVO.ConsultaRegistro model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inscription = await _context.Inscripcion
                .Join(_context.Registro, ins => ins.IdInscripcion, reg => reg.IdInscripcion, (ins, reg) => new { ins, reg })                
                .Join(_context.Contacto,
                 conbinedEnt => conbinedEnt.ins.IdContacto, con => con.IdContacto, (conbinedEnt, con) => new {
                     id = conbinedEnt.ins.IdEvento,
                     fecha = conbinedEnt.ins.Fecha,
                     Motivo = conbinedEnt.ins.Motivo,
                     Nombres = con.Nombres,
                     Apellidos = con.Apellidos,
                     Email = con.CorreoElectronico,
                     Direccion = con.Direccion,
                     Telefono = con.TelefonoMovil,
                     TipoRegistro = conbinedEnt.reg.Descripcion
                 })
                .Where(s => s.id == model.IdEvento && s.TipoRegistro.Trim() == model.TipoInscripcion.Trim() )
                .ToListAsync();

            List<EntityVO.InscritosVO> inscritosVOs = new List<EntityVO.InscritosVO>();
            foreach (var item in inscription)
            {
                EntityVO.InscritosVO inscritosVO = new EntityVO.InscritosVO
                {
                    Apellidos = item.Apellidos,
                    Direccion = item.Direccion,
                    Email = item.Email,
                    Fecha = item.fecha.ToString(),
                    Id = Convert.ToInt32(item.id),
                    Motivo = item.Motivo,
                    Nombres = item.Nombres,
                    Telefono = item.Telefono
                };
                inscritosVOs.Add(inscritosVO);
            }
            if (inscritosVOs == null)
            {
                return NotFound();
            }

            return Ok(inscritosVOs);
        }
    }
}