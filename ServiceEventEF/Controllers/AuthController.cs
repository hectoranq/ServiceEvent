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


namespace ServiceEventEF.Controllers
{
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;
        private IHttpContextAccessor _accessor;
        private readonly IConfiguration _config;
        public AuthController(DB_9AE8B0_GeventDlloContext context, IConfiguration config, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
            _config = config;
        }


        [HttpPost("token")]
        public IActionResult CreateToken([FromBody] CredentialsModel model)
        {
            if (model == null)
            {
                return BadRequest("Request is Null");
            }

            Users users = _context.Users.Where( x => x.Login.ToLower().Equals(model.Username) && x.State.Trim() == "activo" ).FirstOrDefault();

            if (users == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(users.PasswordSalt))
            {
                return BadRequest(new
                {
                    message = "No se encontro ningun usuario con el nombre:" + model.Username
                });
            }
            model.Password = PasswordHasherService.HashPassword(model.Password, users.PasswordSalt);

            var findusr = _context.Users.Where(x => x.Login.ToLower().Equals(model.Username) && x.CryptedPassword.ToLower().Equals(model.Password) ).FirstOrDefault();
            //var findusr = service.Consulta(model.Username, model.Password);
            if (findusr != null)
            {

                var claims = new[]
                {
                          new Claim(JwtRegisteredClaimNames.Sub, findusr.Login),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim("SuperUser", findusr.Role),
                          new Claim("User", findusr.Role)

                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                var token = new JwtSecurityToken(
                  issuer: _config["Tokens:Issuer"],
                  audience: _config["Tokens:Audience"],
                  claims: claims,
                  expires: DateTime.UtcNow.AddMinutes(600),
                  signingCredentials: creds
                  );

                 string token_data = new JwtSecurityTokenHandler().WriteToken(token);
                findusr.PersistenceToken = "A";
                _context.Entry(findusr).State = EntityState.Modified;
                // _context.Users.Add(findusr);
                bool guarda = false;
                try
                {
                     _context.SaveChanges();
                    guarda = true;
                }
                catch (DbUpdateConcurrencyException)
                {
                        return NotFound();
                }

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    isGuarda = guarda,
                    id = findusr.Id,
                    role = findusr.Role
                });
            }
            return BadRequest("Failed to generate Token");
        }


        [HttpPost("email")]
        public IActionResult CreateEmailToken([FromBody] CredentialsModel model)
        {
            if (model == null)
            {
                return BadRequest("Request is Null");
            }

            Users users = _context.Users.Where(x => x.Email.Trim().Equals(model.Username) && x.State.Trim() == "active").FirstOrDefault();

            if (users == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(users.PasswordSalt))
            {
                return BadRequest(new
                {
                    message = "No se encontro ningun usuario con el nombre:" + model.Username
                });
            }
            model.Password = PasswordHasherService.HashPassword(model.Password, users.PasswordSalt);

            var findusr = _context.Users.Where(x => x.Email.Trim().Equals(model.Username) && x.CryptedPassword.ToLower().Equals(model.Password)).FirstOrDefault();
            //var findusr = service.Consulta(model.Username, model.Password);
            if (findusr != null)
            {

                var claims = new[]
                {
                          new Claim(JwtRegisteredClaimNames.Sub, findusr.Login),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim("SuperUser", findusr.Role),
                          new Claim("User", findusr.Role)

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

                string token_data = new JwtSecurityTokenHandler().WriteToken(token);
                findusr.PersistenceToken = "A";
                _context.Entry(findusr).State = EntityState.Modified;
                // _context.Users.Add(findusr);
                bool guarda = false;
                try
                {
                    _context.SaveChanges();
                    guarda = true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    isGuarda = guarda,
                    id = findusr.Id,
                    role = findusr.Role
                });
            }
            return BadRequest("Failed to generate Token");
        }

        // GET: api/Auth
        [HttpGet("users")]
        public IEnumerable<Users> GetUsers()
        {
            return _context.Users;
        }
        #region ESTADISTICA
        // GET: api/Auth
        [HttpGet("nroevento/{id}")]
        public IActionResult NroEventos(int id)
        {
            return Ok(_context.Evento.Where(x => x.IdUsuario == id).Count());
        }

        // GET: api/Auth
        [HttpGet("nrocontactos/{id}")]
        public IActionResult NroContactos(int id)
        {
            return Ok(_context.Contacto.Where(x => x.IdUsuario == id).Count());
        }


        #endregion

        // GET: api/auth/google/{email}
        [HttpPost("google", Name = "GetUsersEmail")]
        public IActionResult GetUsersEmail([FromBody] CredentialsModel email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var findusr = _context.Users.Where(x => x.Email.Trim() == email.Username.Trim()).FirstOrDefault();
                if (findusr != null)
                {

                    var claims = new[]
                    {
                          new Claim(JwtRegisteredClaimNames.Sub, findusr.Login),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim("SuperUser", findusr.Role),
                          new Claim("User", findusr.Role)

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

                    string token_data = new JwtSecurityTokenHandler().WriteToken(token);
                    findusr.PersistenceToken = "A";
                    _context.Entry(findusr).State = EntityState.Modified;
                    // _context.Users.Add(findusr);
                    bool guarda = false;
                    try
                    {
                        _context.SaveChanges();
                        guarda = true;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return NotFound();
                    }

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        isGuarda = guarda, 
                        id = findusr.Id,
                        role = findusr.Role
                    });
                }

                return Ok();   
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }




        }
        // GET: api/Auth/5
        [HttpGet("user/{id}", Name = "GetUsers")]
        public IActionResult GetUsers( int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var users = _context.Users.Where( x => x.Id == id).FirstOrDefault();
                if (users == null)
                {
                    return NotFound();
                }


                return Ok(users);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

            
            

        }

        // PUT: api/Auth/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers([FromRoute] int id, [FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NotFound();
        }



        // POST: api/Auth
        [HttpPost("createuser")]
        public async Task<IActionResult> PostUsers([FromBody] appUser appusers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (this.UsersFindEmail(appusers.email))
                {
                    return NotFound();
                }
                //  var user = mappingUser(appusers);
                bool iscambioPass = true;
                if (appusers.crypted_password != "" && appusers.crypted_password != null)
                {

                    appusers.password_salt = PasswordHasherService.GenerateSalt();
                    //Salt = PasswordHasherService.GenerateSalt(),
                    appusers.crypted_password = PasswordHasherService.HashPassword(appusers.crypted_password, appusers.password_salt);

                } else {
                    iscambioPass = false;
                }


                appusers.current_login_ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                if (this.UsersExists(appusers.id))
                {
                    Users contexUser = this.UsersFind(appusers.id);
                    contexUser.Name = appusers.name;
                    contexUser.Role = appusers.role;
                    contexUser.Surname = appusers.surname;
                    contexUser.State = appusers.state;
                    contexUser.Email = appusers.email;

                    if (iscambioPass)
                    {
                        contexUser.PasswordSalt = appusers.password_salt;
                        contexUser.CryptedPassword = appusers.crypted_password;
                    }

                   // contexUser.Login = appusers.login;
                    //user.Login = contexUser.Login;
                    _context.Entry(contexUser).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                } else {
                    var user = mappingUser(appusers);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }
                return CreatedAtAction("GetUsers", new { id = 0, description = "success" });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
          
        }

        // DELETE: api/Auth/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = _context.Users.Where(x => x.Id == id).FirstOrDefault(); ;
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return Ok(users);
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private Users UsersFind(int id)
        {
            return _context.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        private bool UsersFindEmail(string email)
        {
            return _context.Users.Any(x => x.Email.Trim() == email.Trim());
        }

        private Users mappingUser(appUser appuser) {
            Users user = new Users
            {

                CompanyId = appuser.company_id,
                ConcesionariaId = appuser.concesionaria_id,
                CryptedPassword = appuser.crypted_password,
                CurrentLoginAt = DateTime.Now,
                CurrentLoginIp = appuser.current_login_ip,
                Email = appuser.email,
                GroupId = appuser.group_id,
                Id = appuser.id,
                InReportMail = appuser.in_report_mail,
                LastLoginAt = DateTime.Now,
                LastLoginIp = appuser.last_login_ip,
                LastRequestAt = DateTime.Now,
                Login = appuser.login,
                LoginCount = appuser.login_count,
                Name = appuser.name,
                PasswordSalt = appuser.password_salt,
                PersistenceToken = "",
                Role = appuser.role,
                SingleAccessToken = "",
                State = "active",
                Surname = appuser.surname
            };

            return user;
        }
    }
}