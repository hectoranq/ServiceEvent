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
    [Route("api/contacs")]
    public class ContactoController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public ContactoController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/Contacto
        [HttpGet]
        public IEnumerable<Contacto> GetContacto()
        {
            return _context.Contacto;
        }


        // GET: api/Contacto
        [HttpGet("lista")]
        public IActionResult ListaContacto()
        {
            try
            {
                var contacto = _context.Contacto;
                List<ParametroVO> param = new List<ParametroVO>();
                foreach (var item in contacto)
                {
                    ParametroVO parametroVO = new ParametroVO {
                        descripcion = item.Nombres.Trim() +" "+ item.Apellidos.Trim(),
                        id = item.IdContacto
                    };
                    param.Add(parametroVO);
                }

                return Ok(param);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            
        }

        // GET: api/Contacto/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContacto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contacto = await _context.Contacto.FindAsync(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return Ok(contacto);
        }

        // GET: api/Contacto/5
        [HttpGet("userid/{id}")]
        public async Task<IActionResult> GetContactoForUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contacto = await _context.Contacto.Where(x => x.IdUsuario ==  id).FirstOrDefaultAsync();

            if (contacto == null)
            {
                return NotFound();
            }

            return Ok(contacto);
        }

        // GET: contacto informacion
        [HttpGet("contactoins/{id}")]
        public async Task<IActionResult> GetContactoInscrito([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contacto = await _context.Contacto.Where(x => x.IdUsuario == id).ToListAsync();
            List<EntityVO.ContantoEstadoVO> listContacto = new List<ContantoEstadoVO>();

            foreach (var item in contacto)
            {
                EntityVO.ContantoEstadoVO contantoEstado = new ContantoEstadoVO {
                    FechaCreacion = item.FechaCreacion.ToString(),
                    Inscrito = this.InscritoExists(item.IdContacto),
                    NombreCompleto = item.Nombres.Trim()+" "+ item.Apellidos.Trim()
                };
                listContacto.Add(contantoEstado);
            }
            if (listContacto == null)
            {
                return NotFound();
            }

            return Ok(listContacto);
        }

        // GET solo con el usuario
        [HttpGet("listar/{id}")]
        public async Task<IActionResult> GetContactoListar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contacto = await _context.Contacto.Where(x => x.IdUsuario == id).ToListAsync();

            if (contacto == null)
            {
                return NotFound();
            }

            return Ok(contacto);
        }

        // PUT: api/Contacto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacto([FromRoute] int id, [FromBody] Contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contacto.IdContacto)
            {
                return BadRequest();
            }

            _context.Entry(contacto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactoExists(id))
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

        // POST: api/Contacto
        [HttpPost("create")]
        public async Task<IActionResult> PostContacto([FromBody] DTOs.ContactoDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (ContactoCIExists(model.DocumentoIdentidad))
                {
                    BadRequest(new { Identidad = model.DocumentoIdentidad,
                        Descripcion = "El carnet de identidad ya existe"
                    });
                }


                Contacto contacto = new Contacto
                {
                    Apellidos = model.Apellidos,
                    Cargo = model.Cargo,
                    Categoria = model.Categoria,
                    CorreoElectronico = model.CorreoElectronico,
                    Direccion = model.Direccion,
                    DocumentoIdentidad = model.DocumentoIdentidad,
                    EmpresaNombre = model.EmpresaNombre,
                    Estado = 1,
                    IdEmpresa = model.IdEmpresa,
                    IdUsuario = model.IdUsuario,
                    Nombres = model.Nombres,
                    Pais = model.Pais,
                    Profesion = model.Profesion,
                    TelefonoFijo = model.TelefonoFijo,
                    TelefonoMovil = model.TelefonoMovil,
                    FechaCreacion = DateTime.Now
                };

                _context.Contacto.Add(contacto);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetContacto", new { id = contacto.IdContacto }, contacto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

           
        }

        // DELETE: api/Contacto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contacto = await _context.Contacto.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }

            _context.Contacto.Remove(contacto);
            await _context.SaveChangesAsync();

            return Ok(contacto);
        }

        private bool ContactoExists(int id)
        {
            return _context.Contacto.Any(e => e.IdContacto == id);
        }

        private bool ContactoCIExists(string ci)
        {
            return _context.Contacto.Any(e => e.DocumentoIdentidad == ci);
        }

        //recibe  api/Contacto/correo/5
        [HttpGet("correo/{id}")]
        public IActionResult GetContactoCorreo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactoid = id;

            if (contactoid == 0)
            {
                return NotFound();
            }

            return Ok("Correo de notificacion enviado");
        }

        //Metodos para el usuario web 

        [HttpGet("verificausuario/{id}")]
        public async Task<IActionResult> GetContactoUsuario([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contacto = await _context.Contacto.Where(x => x.IdUsuario == id).FirstOrDefaultAsync();
            if (contacto == null)
            {
                return NotFound();
            }

            return Ok(contacto);
        }

        private bool InscritoExists(int id)
        {
            return _context.Inscripcion.Any(e => e.IdContacto == id);
        }


    }
}