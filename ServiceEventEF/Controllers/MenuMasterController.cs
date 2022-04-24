using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceEventEF.Models;
using ServiceEventEF.EntityVO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceEventEF.Controllers
{
    [Produces("application/json")]
    [Route("api/menuopction")]
    public class MenuMasterController : Controller
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public MenuMasterController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var menu = _context.MenuMaster.Where(x => x.UserRoll == id).ToList();
            if (menu == null)
            {
                return NoContent();
            }

            List<MenuOpcionVO> listaMenu = new List<MenuOpcionVO>();

            foreach (var item in menu)
            {
                MenuOpcionVO menuOpcion = new MenuOpcionVO {
                    Link = item.MenuFileName,
                    Name = item.MenuName,
                    User = item.UserRoll,
                    Logo = item.LogoMenu.Trim()
                    
                };
                listaMenu.Add(menuOpcion);
            }

            return Ok(listaMenu);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
