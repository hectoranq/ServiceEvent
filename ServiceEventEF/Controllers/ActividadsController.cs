using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceEventEF.Models;
using ServiceEventEF.EntityVO;
using System.Globalization;

namespace ServiceEventEF.Controllers
{
    [Produces("application/json")]
    [Route("api/activity")]
    public class ActividadsController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public ActividadsController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/Actividads
        [HttpGet]
        public IActionResult GetActividad()
        {
            var result = _context.Actividad.Where(s => s.Estado == 1);
            //  est_repo.setting = config;
            if (result == null)
            {
                return NotFound();
            }

            List<ActividadsDTO> prs_evento = new List<ActividadsDTO>();
            foreach (var item in result)
            {
                /*
                ActividadsVO actividadVO = new ActividadsVO
                {
                    Ciudad = item.Ciudad,
                    Conferencista = item.Conferencista,
                    CupoMaximoInscripciones = item.CupoMaximoInscripciones,
                    Descripcion = item.Descripcion,
                    Direccion = item.Direccion,
                    Estado = item.Estado,
                    FechaFin = Convert.ToDateTime( item.FechaFin).ToString("dd/MM/yyyy, hh:mm tt", CultureInfo.CreateSpecificCulture("en-US")),
                    FechaInicio = Convert.ToDateTime(item.FechaFin).ToString("dd/MM/yyyy, hh:mm tt", CultureInfo.CreateSpecificCulture("en-US")),
                    IdActividad = item.IdActividad,
                    IdEvento = item.IdEvento,
                    Lugar = item.Lugar,
                    Moderador = item.Moderador,
                    Nombre = item.Nombre,
                    Pais = item.Pais,
                    Predecesor = item.Predecesor,
                    Costo = Convert.ToDecimal( item.CostoActividad)
                };*/
                var contactoConf = _context.Contacto.Where(x => x.IdContacto == item.Conferencista).FirstOrDefault();
                string nombreC = contactoConf.Nombres.Trim() + " " + contactoConf.Apellidos.Trim();
                var contactoExp = _context.Contacto.Where(x => x.IdContacto == item.Moderador).FirstOrDefault();
                string nombreMod = contactoExp.Nombres.Trim() + " " + contactoExp.Apellidos.Trim();
                EntityVO.ActividadsDTO actividads = new ActividadsDTO
                {
                    Ciudad = item.Ciudad,
                    Conferencista = nombreC,
                    CupoMaximoInscripciones = item.CupoMaximoInscripciones,
                    Descripcion = item.Descripcion,
                    Direccion = item.Direccion,
                    Estado = item.Estado,
                    FechaFin = Convert.ToDateTime(item.FechaFin).ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US")) + " "  +item.HoraFin,
                    FechaInicio = Convert.ToDateTime(item.FechaFin).ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"))+" "+ item.HoraInicio,
                    IdActividad = item.IdActividad,
                    IdEvento = item.IdEvento,
                    Lugar = item.Lugar,
                    Moderador = nombreMod,
                    Nombre = item.Nombre,
                    Pais = item.Pais,
                    Predecesor = item.Predecesor,
                    Costo = Convert.ToDecimal(item.CostoActividad)
                };

                prs_evento.Add(actividads);
            }

            return Ok(prs_evento);
        }

        // GET: api/Actividads/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActividad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actividad = await _context.Actividad.FindAsync(id);

            if (actividad == null)
            {
                return NotFound();
            }

            return Ok(actividad);
        }

        // GET: api/Actividads/5
        [HttpGet("actievent/{id}")]
        public async Task<IActionResult> GetActividadEventoParam([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actividad = await _context.Actividad.Where(x => x.IdEvento == id).ToListAsync();

            List<EntityVO.ActividadParam> paramEvento = new List<ActividadParam>();

            foreach (var item in actividad)
            {
                EntityVO.ActividadParam param = new ActividadParam {
                    IdActividad = item.IdActividad,
                    NombreActividad = item.Nombre
                };
                paramEvento.Add(param);
            }


            if (paramEvento == null)
            {
                return NotFound();
            }

            return Ok(paramEvento);
        }



        // GET: api/Actividads/5
        [HttpGet("evento/{id}")]
        public IActionResult GetActividadEvento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actividad =  _context.Actividad.Where(x => x.IdEvento == id).ToList();
            List<EntityVO.ActividadsDTO> lstActividades = new List<ActividadsDTO>();
            foreach (var item in actividad)
            {
                var contactoConf = _context.Contacto.Where(x => x.IdContacto == item.Conferencista).FirstOrDefault();
                string nombreC = contactoConf.Nombres.Trim() + " " + contactoConf.Apellidos.Trim();
                var contactoExp = _context.Contacto.Where(x => x.IdContacto == item.Moderador).FirstOrDefault();
                string nombreMod = contactoExp.Nombres.Trim() + " " + contactoExp.Apellidos.Trim();
                var CategoriaEvento = _context.ActividadCategoria.Where(x => x.IdActividad == item.IdActividad).FirstOrDefault();
                EntityVO.ActividadsDTO actividads = new ActividadsDTO
                {
                    Ciudad = item.Ciudad,
                    Conferencista = nombreC,
                    CupoMaximoInscripciones = item.CupoMaximoInscripciones,
                    Descripcion = item.Descripcion,
                    Direccion = item.Direccion,
                    Estado = item.Estado,
                    FechaFin = Convert.ToDateTime( item.FechaFin).ToString("dd/MM/yyyy"),
                    FechaInicio = Convert.ToDateTime(item.FechaInicio).ToString("dd/MM/yyyy"),
                    IdActividad = item.IdActividad,
                    IdEvento = item.IdEvento,
                    Lugar = item.Lugar,
                    Moderador = nombreMod,
                    Nombre = item.Nombre,
                    Pais = item.Pais,
                    Predecesor = item.Predecesor,
                    Costo = Convert.ToDecimal( item.CostoActividad),
                    HoraFin = item.HoraFin,
                    HoraInicio = item.HoraInicio,
                    IdCategoria = CategoriaEvento.IdCategoria,
                    IdConferencista = item.Conferencista,
                    IdModerador = item.Moderador
                };


                lstActividades.Add(actividads);
            }
            if (lstActividades.Count <= 0)
            {
                return NotFound();
            }

            return Ok(lstActividades);
        }

        // PUT: api/Actividads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActividad([FromRoute] int id, [FromBody] ActividadsVO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.IdActividad)
            {
                return BadRequest();
            }
            try
            {
                string[] arrfechaFin = model.FechaFin.Split('/');
                string[] arrfechaInicio = model.FechaInicio.Split('/');

                DateTime fech_fin = new DateTime(int.Parse(arrfechaFin[2]), int.Parse(arrfechaFin[1]), int.Parse(arrfechaFin[0]));
                DateTime fech_inicio = new DateTime(int.Parse(arrfechaInicio[2]), int.Parse(arrfechaInicio[1]), int.Parse(arrfechaInicio[0]));
                Actividad actividad = new Actividad
                {
                    Ciudad = model.Ciudad,
                    Conferencista = model.Conferencista,
                    CostoActividad = model.Costo,
                    CupoMaximoInscripciones = model.CupoMaximoInscripciones,
                    Descripcion = model.Descripcion,
                    Direccion = model.Direccion,
                    Estado = model.Estado,
                    FechaFin = fech_fin,
                    FechaInicio = fech_inicio,
                    HoraFin = model.HoraFin,
                    HoraInicio = model.HoraInicio,
                    IdActividad = model.IdActividad,
                    IdEvento = model.IdEvento,
                    Lugar = model.Lugar,
                    Moderador = model.Moderador,
                    Nombre = model.Nombre,
                    Pais = model.Pais,
                    Predecesor = model.Predecesor,
                    
                };

                var accate = _context.ActividadCategoria.Where(x => x.IdActividad == model.IdActividad).FirstOrDefault();

                ActividadCategoria actividadCategoria = new ActividadCategoria
                {
                    IdActividadCategoria = accate.IdActividadCategoria,
                    IdEvento = model.IdEvento,
                    IdActividad = model.IdActividad,
                    IdCategoria = model.IdCategoria
                };

                accate.IdEvento = model.IdEvento;
                accate.IdActividad = model.IdActividad;
                accate.IdCategoria = model.IdCategoria;

                _context.Entry(actividad).State = EntityState.Modified;

                if (this.ActividadCategoriaExists(model.IdActividad))
                {
                    _context.Entry(accate).State = EntityState.Modified;
                }
                else {
                    _context.ActividadCategoria.Add(actividadCategoria);
                }

               
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!ActividadExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw new Exception(ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }


            return Ok(new {
                id = id,
                resp = "success"
            });
        }

        // POST: api/Actividads
        [HttpPost]
        public async Task<IActionResult> PostActividad([FromBody] ActividadsVO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Actividad actividad;
            ActividadCategoria actividadCategoria;
            try
            {
                string[] arrfechaFin = model.FechaFin.Split('/');
                string[] arrfechaInicio = model.FechaInicio.Split('/');

                DateTime fech_fin = new DateTime(int.Parse(arrfechaFin[2]), int.Parse(arrfechaFin[1]), int.Parse(arrfechaFin[0]));
                DateTime fech_inicio = new DateTime(int.Parse(arrfechaInicio[2]), int.Parse(arrfechaInicio[1]), int.Parse(arrfechaInicio[0]));
                actividad = new Actividad
                {
                    IdActividad = model.IdActividad,
                    IdEvento = model.IdEvento,
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion,
                    FechaInicio = fech_inicio,
                    FechaFin = fech_fin,
                    Predecesor = model.Predecesor,
                    CupoMaximoInscripciones = model.CupoMaximoInscripciones,
                    Conferencista = model.Conferencista,
                    Moderador = model.Moderador,
                    Lugar = model.Lugar,
                    Direccion = model.Direccion,
                    Ciudad = model.Ciudad,
                    Pais = model.Pais,
                    Estado = model.Estado,
                    CostoActividad = model.Costo,
                    HoraInicio = model.HoraInicio,
                    HoraFin = model.HoraFin
                };

            
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
           

            if (model.IdActividad != 0)
            {
                _context.Entry(actividad).State = EntityState.Modified;
               
            }
            else
            {
                _context.Actividad.Add(actividad);
               
            }

            await _context.SaveChangesAsync();

            if (actividad.IdActividad != 0)
            {
                actividadCategoria = new ActividadCategoria
                {
                    IdActividadCategoria = 0,
                    IdEvento = model.IdEvento,
                    IdActividad = actividad.IdActividad,
                    IdCategoria = model.IdCategoria
                };
                _context.ActividadCategoria.Add(actividadCategoria);
            }


            await _context.SaveChangesAsync();

            return Ok(new { id = actividad.IdActividad, descripcion = "Success"});
        }

        // DELETE: api/Actividads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActividad([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var actividad = await _context.Actividad.FindAsync(id);

            var actividadevento = await _context.ActividadCategoria.Where(x => x.IdActividad == id).ToListAsync();

            foreach (var item in actividadevento)
            {
                _context.ActividadCategoria.Remove(item);
            }

            if (actividad == null)
            {
                return NotFound();
            }

            _context.Actividad.Remove(actividad);



            await _context.SaveChangesAsync();

            return Ok(actividad);
        }

        private bool ActividadExists(int id)
        {
            return _context.Actividad.Any(e => e.IdActividad == id);
        }


        private bool ActividadCategoriaExists(int id)
        {
            return _context.ActividadCategoria.Any(e => e.IdActividad == id);
        }
    }
}