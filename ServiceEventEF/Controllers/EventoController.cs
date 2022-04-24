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
    [Route("api/evento")]
    public class EventoController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public EventoController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        [HttpGet("all/{id}")]
        public IActionResult GetEventoID(int id)
        {
            try
            {
                var result = _context.Evento.Where(s => s.Estado == 1 && s.IdUsuario == id).ToList();
                //  est_repo.setting = config;
                if (result == null)
                {
                    return NotFound();
                }

                List<EventoVO> prs_evento = new List<EventoVO>();
                foreach (var item in result)
                {
                    var actividades = _context.Actividad.Where(x => x.IdEvento == item.IdEvento);
                    try
                    {
                        EventoVO eventoVO = new EventoVO
                        {
                            Ciudad = item.Ciudad,
                            CupoMaximoInscripciones = item.CupoMaximoInscripciones,
                            Descripcion = item.Descripcion,
                            Direccion = item.Direccion,
                            Estado = item.Estado,
                            FechaFin = Convert.ToDateTime(item.FechaFin).ToString("dd/MM/yyyy"),
                            FechaInicio = Convert.ToDateTime(item.FechaInicio).ToString("dd/MM/yyyy"),
                            HoraFin = item.HoraFin,
                            HoraInicio = item.HoraInicio,
                            IdEvento = item.IdEvento,
                            Lugar = (item.Lugar == null) ? "" : item.Lugar,
                            Nombre = (item.Nombre == null) ? "" : item.Nombre,
                            Pais = (item.Pais == "") ? "" : _context.Parametros.Where(s => s.NombreTipo == item.Pais).FirstOrDefault<Parametros>().NombreParametro,
                            Ruta = (item.RutaImagen == null) ? "" : item.RutaImagen,
                            NroActividades = actividades.Count(),
                            Latitud = decimal.Parse(item.Latitud.ToString()),
                            Longitud = decimal.Parse(item.Longitud.ToString())

                        };
                        prs_evento.Add(eventoVO);
                    }
                    catch (Exception ex)
                    {

                    }


                }

                return Ok(prs_evento);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
           
            //return new JsonResult(result);
        }

        // GET: api/Evento/5
        [HttpGet("proximomes")]
        public IActionResult EventosProximosMes() 
        {
            try
            {
                //Primero obtenemos el día actual
                DateTime date = DateTime.Now;

                //Asi obtenemos el primer dia del mes actual
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

                //Y de la siguiente forma obtenemos el ultimo dia del mes
                //agregamos 1 mes al objeto anterior y restamos 1 día.
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

                string primer = oPrimerDiaDelMes.ToString("yyyy/MM/dd");
                string ultimo = oUltimoDiaDelMes.ToString("yyyy/MM/dd");

                string sQuery = "SELECT * FROM Evento WHERE  CONVERT(varchar(10), Fecha_Inicio, 112) BETWEEN '" + primer.Replace("/", "") + "' and '" + ultimo.Replace("/", "") + "'";
                var result = _context.Evento.FromSql(sQuery).ToList();

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(
                    result
                 );
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("anteriorevento")]
        public IActionResult EventosAnteroiresMes()
        {
            try
            {
                //Primero obtenemos el día actual
                DateTime date = DateTime.Now;

                //Asi obtenemos el primer dia del mes actual
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

                //Y de la siguiente forma obtenemos el ultimo dia del mes
                //agregamos 1 mes al objeto anterior y restamos 1 día.
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

                string primer = oPrimerDiaDelMes.ToString("yyyy/MM/dd");
                string ultimo = oUltimoDiaDelMes.ToString("yyyy/MM/dd");

                string sQuery = "SELECT * FROM Evento WHERE  CONVERT(varchar(10), Fecha_Inicio, 112) < '" + primer.Replace("/", "") + "'";
                var result = _context.Evento.FromSql(sQuery).ToList();

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(
                
                    result
                );
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/Evento/5
        [HttpGet("siguientemes")]
        public IActionResult EventosSiguienteMes()
        {
            try
            {
                //Primero obtenemos el día actual
                DateTime date = DateTime.Now;

                //Asi obtenemos el primer dia del mes actual
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month + 1 , 1);

                //Y de la siguiente forma obtenemos el ultimo dia del mes
                //agregamos 1 mes al objeto anterior y restamos 1 día.
                DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

                string primer = oPrimerDiaDelMes.ToString("yyyy/MM/dd");
                string ultimo = oUltimoDiaDelMes.ToString("yyyy/MM/dd");
                string sQuery = "SELECT * FROM Evento WHERE  CONVERT(varchar(10), Fecha_Inicio, 112) BETWEEN '" + primer.Replace("/", "") + "' and '" + ultimo.Replace("/", "") + "'";
                var result = _context.Evento.FromSql(sQuery).ToList();

                if (result == null)
                {
                    return NotFound();
                }
              //  var filtrado = result.Find(x => x.FechaInicio < oUltimoDiaDelMes && oPrimerDiaDelMes > x.FechaInicio);
                return Ok(
                
                    result
                );
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> GetEventoUsuarioInscripcion([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var inscription = await _context.Inscripcion.Join(_context.Evento,
               ins => ins.IdEvento, con => con.IdEvento, (ins, con) => new {
                   id = ins.IdEvento,
                   idUsuario = con.IdUsuario,
                   idIncripcion = ins.IdInscripcion,
                   FechaInscripcion = ins.Fecha,
                   Motivo = ins.Motivo,
                   NombreEvento = con.Nombre,
                   Descripcion = con.Descripcion,
                   Direccion = con.Direccion,
                   
               })
              .Where(s => s.idUsuario == id)
              .ToListAsync();

            if (inscription == null)
            {
                return NotFound();
            }
            return Ok(inscription);
        }


        [HttpGet("sininscripcion/{id}")]
        public async Task<IActionResult> GetEventoSinInscripcion([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }


            var inscription = await _context.Inscripcion.Join(_context.Evento,
               ins => ins.IdEvento, con => con.IdEvento, (ins, con) => new {
                   id = ins.IdEvento,
                   idUsuario = con.IdUsuario,
                   idIncripcion = ins.IdInscripcion,
                   FechaInicio = con.FechaInicio,
                   FechaFin = con.FechaFin,
                   NombreEvento = con.Nombre,
                   Descripcion = con.Descripcion,
                   Direccion = con.Direccion,

               })
              .Where(s => s.idUsuario != id && s.FechaInicio >= DateTime.Now )
              .ToListAsync();

            if (inscription == null)
            {
                return NotFound();
            }
            return Ok(inscription);
        }

        // GET: api/Evento/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var evento = await _context.Evento.FindAsync(id);

            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        [HttpGet("lista/{id}")]
        public IActionResult ListaEvento(int id)
        {
            try
            {
                var events = _context.Evento.Where(x => x.IdUsuario == id && x.Estado == 1).ToList();
                List<ParametroEventoVO> param = new List<ParametroEventoVO>();
                foreach (var item in events)
                {
                    ParametroEventoVO parametroVO = new ParametroEventoVO
                    {
                        descripcion = item.Descripcion,
                        id = item.IdEvento,
                        nombre = item.Nombre
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

        // PUT: api/Evento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento([FromRoute] int id, [FromBody] Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != evento.IdEvento)
            {
                return BadRequest();
            }

            _context.Entry(evento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
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

        // POST: api/Evento
        [HttpPost("create")]
        public async Task<IActionResult> PostEvento([FromBody] EventoVO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string[] arrfechaFin = model.FechaFin.Split('/');
                string[] arrfechaInicio = model.FechaInicio.Split('/');

                DateTime fech_fin = new DateTime( int.Parse( arrfechaFin[2]),int.Parse(arrfechaFin[1] ), int.Parse(arrfechaFin[0]));
                DateTime fech_inicio = new DateTime(int.Parse(arrfechaInicio[2]), int.Parse(arrfechaInicio[1]), int.Parse(arrfechaInicio[0]));

                Evento evento = new Evento
                {
                    Ciudad = model.Ciudad,
                    CupoMaximoInscripciones = model.CupoMaximoInscripciones,
                    Descripcion = model.Descripcion,
                    Direccion = model.Direccion,
                    Estado = model.Estado,
                    FechaFin = fech_fin,
                    FechaInicio = fech_inicio,
                    IdEvento = model.IdEvento,
                    Lugar = model.Lugar,
                    Nombre = model.Nombre,
                    Pais = model.Pais,
                    RutaImagen = model.Ruta,
                    Latitud = model.Latitud,
                    Longitud = model.Longitud,
                    IdUsuario = model.IdUsuario,
                    HoraInicio = model.HoraInicio,
                    HoraFin = model.HoraFin
                };

                if (evento.IdEvento != 0)
                {
                    _context.Entry(evento).State = EntityState.Modified;
                }
                else
                {
                    _context.Evento.Add(evento);
                }


                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEvento", new { id = evento.IdEvento }, evento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        // DELETE: api/Evento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            _context.Evento.Remove(evento);
            await _context.SaveChangesAsync();

            return Ok(evento);
        }

        private bool EventoExists(int id)
        {
            return _context.Evento.Any(e => e.IdEvento == id);
        }
    }
}