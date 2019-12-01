using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollAPI.Models;

namespace PollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VriendController : ControllerBase
    {
        private readonly PollContext _context;

        public VriendController(PollContext context)
        {
            _context = context;
        }

        // GET: api/Vriend
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vriend>>> GetVrienden()
        {
            return await _context.Vrienden.ToListAsync();
        }

        [Authorize]
        [HttpGet("gebruiker/{gebruikerId}")]
        public async Task<ActionResult<IEnumerable<Vriend>>> GetVriendenWhereOntvangerId(int gebruikerId)
        {
            return await _context.Vrienden.Where(c => c.OntvangerId == gebruikerId).ToListAsync();
        }       
        
        [Authorize]
        [HttpGet("verzender/{verzenderId}")]
        public async Task<ActionResult<IEnumerable<Vriend>>> GetVriendenWhereId(int verzenderId)
        {
            return await _context.Vrienden.Where(c => c.VerzenderId == verzenderId).ToListAsync();
        }

        // GET: api/Vriend/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Vriend>> GetVriend(int id)
        {
            var vriend = await _context.Vrienden.FindAsync(id);

            if (vriend == null)
            {
                return NotFound();
            }

            return vriend;
        }

        // PUT: api/Vriend/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVriend(int id, Vriend vriend)
        {
            if (id != vriend.VriendId)
            {
                return BadRequest();
            }

            _context.Entry(vriend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VriendExists(id))
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

        // POST: api/Vriend
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vriend>> PostVriend(Vriend vriend)
        {
            _context.Vrienden.Add(vriend);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVriend", new { id = vriend.VriendId }, vriend);
        }

        // DELETE: api/Vriend/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vriend>> DeleteVriend(int id)
        {
            var vriend = await _context.Vrienden.FindAsync(id);
            if (vriend == null)
            {
                return NotFound();
            }

            _context.Vrienden.Remove(vriend);
            await _context.SaveChangesAsync();

            return vriend;
        }

        private bool VriendExists(int id)
        {
            return _context.Vrienden.Any(e => e.VriendId == id);
        }
    }
}
