using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceEventEF.Models;
using System.Globalization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServiceEventEF.Controllers
{
    [Produces("application/json")]
    [Route("api/inscription")]
    public class InscripcionsController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;
        private readonly ILogger<InscripcionsController> _logger;
        public InscripcionsController(DB_9AE8B0_GeventDlloContext context, ILogger<InscripcionsController> logger)
        {
            _context = context;
            _logger = logger;

        }

        // GET: api/Inscripcions
        [HttpGet]
        public IEnumerable<Inscripcion> GetInscripcion()
        {
            return _context.Inscripcion;
        }

        // GET: api/Inscripcions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInscripcion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inscripcion = await _context.Inscripcion.FindAsync(id);

            if (inscripcion == null)
            {
                return NotFound();
            }

            return Ok(inscripcion);
        }



        [HttpGet("valida/{id}")]
        public async Task<IActionResult> GetInscripcionEvento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inscription = _context.Inscripcion.Select(s => new
            {
                idcontacto = s.IdContacto,
                idevento = s.IdEvento
            }).
                Where(x => x.idevento == id).Distinct().ToList();



            List<EntityVO.InscritosVO> inscritosVOs = new List<EntityVO.InscritosVO>();
            foreach (var ins in inscription)
            {
                var item = _context.Contacto.Where(x => x.IdContacto == ins.idcontacto).FirstOrDefault();
                EntityVO.InscritosVO inscritosVO = new EntityVO.InscritosVO {
                    Apellidos = item.Apellidos,
                    Direccion = item.Direccion,
                    Email = item.CorreoElectronico,
                    Fecha = DateTime.Now.ToString(),
                    Id = Convert.ToInt32( item.IdContacto),
                    Motivo = "",
                    Nombres = item.Nombres,
                    Telefono = item.TelefonoMovil,
                    IdInscripcion = 0,
                    NroIdentidad = item.DocumentoIdentidad,
                    isRegistro = false
                };
                inscritosVOs.Add(inscritosVO);
            }
            if (inscritosVOs == null)
            {
                return NotFound();
            }

            return Ok(inscritosVOs);
        }

        [HttpPost("validaact")]
        public IActionResult ValidaInscripcionActividad([FromBody] EntityVO.ValidaInscripcion model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation(JsonConvert.SerializeObject(model));
            try
            {
                var inscripcion = _context.Inscripcion.Where(x => x.IdEvento == model.IdEvento
                && x.IdActividad == model.IdActividad && x.IdContacto == model.IdContacto).FirstOrDefault();

                if (inscripcion == null)
                {
                    _logger.LogInformation("OBJETO NULO");
                    return NotFound("Inscripcio es nulo o vacio");
                }
                var contacto = _context.Contacto.Where(x => x.IdContacto == model.IdContacto).FirstOrDefault();

                return Ok(new EntityVO.RegistroAsistencia
                {
                    Estado = "INSCRITO",
                    NombreCompleto = contacto.Nombres.Trim() + " " + contacto.Apellidos.Trim(),
                    NroDocumento = contacto.DocumentoIdentidad,
                    IdInscripcion = inscripcion.IdInscripcion
                }) ;
                
            }
            catch (Exception ex)
            {
                _logger.LogError("[ERROR]"+ex.ToString());
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("evgcontact")]
        public async Task<IActionResult> GetAllInsEventoContacto([FromBody] DTOs.InscripcionQRDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inscription = await _context.Inscripcion.Join(_context.Contacto,
                 ins => ins.IdContacto, con => con.IdContacto, (ins, con) => new {
                     id = ins.IdEvento,
                     IdContacto = ins.IdContacto,
                     IdInscripcion = ins.IdInscripcion,
                     fecha = ins.Fecha,
                     Motivo = ins.Motivo,
                     Nombres = con.Nombres,
                     Apellidos = con.Apellidos,
                     Email = con.CorreoElectronico,
                     Direccion = con.Direccion,
                     Telefono = con.TelefonoMovil,
                     IdPago = (ins.IdPago == null)?0: ins.IdPago
                 })
                .Where(s => s.id == model.IdEvento && s.IdContacto == model.IdUsuario)
                .FirstOrDefaultAsync();

       
            if (inscription == null)
            {
                return NotFound();
            }

            return Ok(inscription);
        }


        [HttpPost("actividad")]
        public async Task<IActionResult> GetInscripcionEventoContacto([FromBody] DTOs.InscripcionQRDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inscription = await _context.Inscripcion.Join(_context.Actividad,
                 ins => ins.IdActividad, con => con.IdActividad, (ins, con) => new {
                     Ciudad = con.Ciudad,
                     CupoMaximoInscripciones = con.CupoMaximoInscripciones,
                     Descripcion = con.Descripcion,
                     Direccion = con.Direccion,
                     Estado = con.Estado,
                     FechaFin = Convert.ToDateTime(con.FechaFin).ToString("dd/MM/yyyy, hh:mm tt", CultureInfo.CreateSpecificCulture("en-US")),
                     FechaInicio = Convert.ToDateTime(con.FechaFin).ToString("dd/MM/yyyy, hh:mm tt", CultureInfo.CreateSpecificCulture("en-US")),
                     IdActividad = con.IdActividad,
                     Lugar = con.Lugar,
                     Nombre = con.Nombre,
                     Pais = con.Pais,
                     Predecesor = con.Predecesor,

                     IdEvento = ins.IdEvento,
                     IdContacto = ins.IdContacto
                 })
                .Where(s => s.IdEvento == model.IdEvento && s.IdContacto == model.IdUsuario)
                .ToListAsync();

           
            if (inscription == null)
            {
                return NotFound();
            }

            return Ok(inscription);
        }



        // PUT: api/Inscripcions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscripcion([FromRoute] int id, [FromBody] Inscripcion inscripcion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inscripcion.IdInscripcion)
            {
                return BadRequest();
            }

            _context.Entry(inscripcion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscripcionExists(id))
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

        // POST: api/Inscripcions
        [HttpPost]
        public async Task<IActionResult> PostInscripcion([FromBody] DTOs.IncripcionDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Inscripcion inscripcion = new Inscripcion {
                Acompanante = model.Acompanante,
                EmpresaPatrocinadora = model.EmpresaPatrocinadora,
                Encuesta = model.Encuesta,
                Estado = model.Estado,
                Fecha = DateTime.Now,
                IdActividad = model.IdActividad,
                IdContacto = model.IdContacto,
                IdEvento = model.IdEvento,
                IdInscripcion = model.IdInscripcion,
                IdPago = model.IdPago,
                Motivo = model.Motivo,
                Notificaciones = model.Notificaciones,
                Patrocinio = model.Patrocinio,
                Visitante = model.Visitante
            };
            _context.Inscripcion.Add(inscripcion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInscripcion", new { id = inscripcion.IdInscripcion }, inscripcion);
        }

        // DELETE: api/Inscripcions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscripcion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inscripcion = await _context.Inscripcion.FindAsync(id);
            if (inscripcion == null)
            {
                return NotFound();
            }

            _context.Inscripcion.Remove(inscripcion);
            await _context.SaveChangesAsync();

            return Ok(inscripcion);
        }

        private bool InscripcionExists(int id)
        {
            return _context.Inscripcion.Any(e => e.IdInscripcion == id);
        }

        private bool RegistroExists(int id)
        {
            return _context.Registro.Any(e => e.IdInscripcion == id);
        }
    }
}