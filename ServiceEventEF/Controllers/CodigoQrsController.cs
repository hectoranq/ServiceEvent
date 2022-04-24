using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceEventEF.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

using System.Drawing;
using System.IO;
using QRCoder;
using Newtonsoft.Json;
namespace ServiceEventEF.Controllers
{
    [Produces("application/json")]
    [Route("api/codigoqr")]
    public class CodigoQrsController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;
        private readonly IConfiguration _config;

        public CodigoQrsController(DB_9AE8B0_GeventDlloContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/CodigoQrs
        [HttpGet]
        public IEnumerable<CodigoQr> GetCodigoQr()
        {
            return _context.CodigoQr;
        }

        // GET: api/CodigoQrs/5
        [HttpGet("{guid}")]
        public IActionResult GetCodigoQr([FromRoute] string guid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var qr = _context.CodigoQr.Where(x => x.Nombre.Trim() == guid.Trim()).FirstOrDefault();


            string secret = _config["Tokens:Key"];
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters()
            {
                ValidIssuer = _config["Tokens:Issuer"],
                ValidAudience = _config["Tokens:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"])),
                ValidateLifetime = true
            };
            var claims = handler.ValidateToken(qr.Descripcion, validations, out var tokenSecure);
            DTOs.InscripcionQRDTO inscripcionQRDTO = new DTOs.InscripcionQRDTO();
            foreach (var claimip in claims.Claims)
            {
                if (claimip.Type == "idEvento")
                {
                    inscripcionQRDTO.IdEvento = int.Parse( claimip.Value);
                }

                if (claimip.Type == "idUsuario")
                {
                    inscripcionQRDTO.IdUsuario = int.Parse(claimip.Value);
                }
            }
            return Ok(inscripcionQRDTO);
        }

        // PUT: api/CodigoQrs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCodigoQr([FromRoute] int id, [FromBody] CodigoQr codigoQr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != codigoQr.IdCodigoQr)
            {
                return BadRequest();
            }

            _context.Entry(codigoQr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodigoQrExists(id))
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

        // POST: api/CodigoQrs
        [HttpGet("evento/{idevento}/usuario/{idusuario}")]
        public async Task<IActionResult> PostCodigoQr(int idevento, int idusuario)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            #region modulo de creacion
            /*
           #region Generacion TOken
           string token_data = string.Empty;
           try
           {
               var claims = new[]
                   {
                         new Claim(JwtRegisteredClaimNames.Sub, "Inscripcion"),
                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                         new Claim("idEvento", idevento.ToString()),
                         new Claim("idUsuario", idusuario.ToString())

                   };

               var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
               var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

               var token = new JwtSecurityToken(
                 issuer: _config["Tokens:Issuer"],
                 audience: _config["Tokens:Audience"],
                 claims: claims,
                 expires: DateTime.UtcNow.AddMinutes(12),
                 signingCredentials: creds
                 );

               token_data = new JwtSecurityTokenHandler().WriteToken(token);
           }
           catch (Exception ex)
           {
               return NotFound("Error generar token:" + ex.Message);
           }
           #endregion
           string QRNombre = Guid.NewGuid().ToString();
           CodigoQr codigoQr = new CodigoQr();

           codigoQr.Nombre = QRNombre;
           codigoQr.Descripcion = token_data;
           _context.CodigoQr.Add(codigoQr);
           await _context.SaveChangesAsync();
           */
            #endregion
            var item = _context.Inscripcion.Where(x => x.IdEvento == idevento && x.IdContacto == idusuario).FirstOrDefault();
            
                DTOs.InscripcionActividadDTO incripcionDTOSS = new DTOs.InscripcionActividadDTO
                {
                    
                    Estado = int.Parse(item.Estado.ToString()),
                    
                    IdContacto = int.Parse(item.IdContacto.ToString()),
                    IdEvento = int.Parse(item.IdEvento.ToString()),
                    
                    IdPago = int.Parse(item.IdPago.ToString()),
                    
                };
               
            
            string url_qr = JsonConvert.SerializeObject(incripcionDTOSS); 
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url_qr,
            QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            //return BitmapToBytes(qrCodeImage);

            return File(BitmapToStream(qrCodeImage), "image/png", "Inscrito"+ item.IdInscripcion + ".png");
        }

        private static Byte[] BitmapToStream(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                return stream.ToArray();
            }
        }


 

        // DELETE: api/CodigoQrs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCodigoQr([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var codigoQr = await _context.CodigoQr.FindAsync(id);
            if (codigoQr == null)
            {
                return NotFound();
            }

            _context.CodigoQr.Remove(codigoQr);
            await _context.SaveChangesAsync();

            return Ok(codigoQr);
        }

        private bool CodigoQrExists(int id)
        {
            return _context.CodigoQr.Any(e => e.IdCodigoQr == id);
        }
    }
}