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
    [Route("api/paymentpayu")]
    public class PaymentsController : ControllerBase
    {
        private readonly DB_9AE8B0_GeventDlloContext _context;

        public PaymentsController(DB_9AE8B0_GeventDlloContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public IEnumerable<Payments> GetPayments()
        {
            return _context.Payments;
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payments = await _context.Payments.FindAsync(id);

            if (payments == null)
            {
                return NotFound();
            }

            return Ok(payments);
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayments([FromRoute] int id, [FromBody] Payments payments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payments.Id)
            {
                return BadRequest();
            }

            _context.Entry(payments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentsExists(id))
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

        // POST: api/Payments
        [HttpPost]
        public async Task<IActionResult> PostPayments([FromBody] Payments payments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Payments.Add(payments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayments", new { id = payments.Id }, payments);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payments = await _context.Payments.FindAsync(id);
            if (payments == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payments);
            await _context.SaveChangesAsync();

            return Ok(payments);
        }

        private bool PaymentsExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}