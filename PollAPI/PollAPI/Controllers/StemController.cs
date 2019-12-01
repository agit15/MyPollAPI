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
    public class StemController : ControllerBase
    {
        private readonly PollContext _context;

        public StemController(PollContext context)
        {
            _context = context;
        }

        // GET: api/Stem
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stem>>> GetStemmen()
        {
            return await _context.Stemmen.ToListAsync();
        }

        [Authorize]
        [HttpGet("antwoord/{antwoordId}")]
        public async Task<ActionResult<IEnumerable<Stem>>> GetStemmenByAntwoordId(int antwoordId)
        {
            return await _context.Stemmen.Where(c => c.AntwoordId == antwoordId).ToListAsync();
        }

        // GET: api/Stem/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Stem>> GetStem(int id)
        {
            var stem = await _context.Stemmen.FindAsync(id);

            if (stem == null)
            {
                return NotFound();
            }

            return stem;
        }

        // PUT: api/Stem/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStem(int id, Stem stem)
        {
            if (id != stem.StemId)
            {
                return BadRequest();
            }

            _context.Entry(stem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StemExists(id))
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

        // POST: api/Stem
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Stem>> PostStem(Stem stem)
        {
            _context.Stemmen.Add(stem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStem", new { id = stem.StemId }, stem);
        }

        // DELETE: api/Stem/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stem>> DeleteStem(int id)
        {
            var stem = await _context.Stemmen.FindAsync(id);
            if (stem == null)
            {
                return NotFound();
            }

            _context.Stemmen.Remove(stem);
            await _context.SaveChangesAsync();

            return stem;
        }

        private bool StemExists(int id)
        {
            return _context.Stemmen.Any(e => e.StemId == id);
        }
    }
}